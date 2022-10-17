using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.Point;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Mapper;
using MozaeekCore.QueryModel;
using MozaeekCore.Common;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{

    public class CreatePointsFromExcelCommandHandler : IBaseAsyncCommandHandler<CreatePointFromExcelCommand, List<CreatePointCommandResult>>
    {
        private readonly IGenericRepository<Domain.Point> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPointQueryService _pointQueryService;

        public CreatePointsFromExcelCommandHandler(IGenericRepository<Domain.Point> repository, IUnitOfWork unitOfWork, IPointQueryService pointQueryService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _pointQueryService = pointQueryService;
        }
        public async Task<List<CreatePointCommandResult>> HandleAsync(CreatePointFromExcelCommand cmd)
        {
            Debug.WriteLine($"{DateTime.Now.ToString("G")}");
            var result = cmd.ExcelPath.ImportExcel();
            Debug.WriteLine($"{DateTime.Now.ToString("G")}");

            var pointList = result.Where(r => (string.IsNullOrEmpty(r.ParentCode) || r.ParentCode == "0")).Select(r =>
            {
                var children = GetAllPointWithChildren(r, result.ToList());

                return new Domain.Point(0, r.Title, children);
            }).ToList();


            await _repository.AddRange(pointList);
            await _unitOfWork.CommitAsync();

            await SaveToQuery(pointList);

            return pointList.Select(p => BasicInfoProfile.GetPointCommandResult(p)).ToList();
        }

        private List<MozaeekCore.Domain.Point> GetAllPointWithChildren(ParentChildExcelDto parent, List<ParentChildExcelDto> list)
        {
            var result = new List<MozaeekCore.Domain.Point>();
            list.Where(x => x.ParentCode == parent.Code).ToList().ForEach(z =>
                {
                    result.Add(new Domain.Point(0, z.Title, GetAllPointWithChildren(z, list)));
                });
            return result;
        }

        private async Task SaveToQuery(List<Domain.Point> list)
        {
            var result = new List<PointQuery>();
            foreach (var p in list)
            {
                await _pointQueryService.Create(new QueryModel.PointQuery(p.Id, p.Title, p.ParentId, DateTime.Now, Guid.NewGuid()));
                if (p.SubPoints != null && p.SubPoints.Any())
                {
                   await SaveToQuery(p.SubPoints.ToList());
                }
            }
        }

    }
}

