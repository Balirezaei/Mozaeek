using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.Point;
using MozaeekCore.Common;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{
    public class CreateLabelsFromExcelCommandHandler : IBaseAsyncCommandHandler<CreateLabelFromExcelCommand, List<LabelDto>>
    {
        private readonly IGenericRepository<Domain.Label> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILabelQueryService _LabelQueryService;

        public CreateLabelsFromExcelCommandHandler(IGenericRepository<Domain.Label> repository, IUnitOfWork unitOfWork, ILabelQueryService LabelQueryService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _LabelQueryService = LabelQueryService;
        }
        public async Task<List<LabelDto>> HandleAsync(CreateLabelFromExcelCommand cmd)
        {
            var result = cmd.ExcelPath.ImportExcel();
            var LabelList = result.Where(r => (string.IsNullOrEmpty(r.ParentCode) || r.ParentCode == "0")).Select(r =>
            {
                var children = GetAllLabelWithChildren(r, result.ToList());

                return new Domain.Label(0, r.Title, children);
            }).ToList();

            await _repository.AddRange(LabelList);
            await _unitOfWork.CommitAsync();

            await SaveToQuery(LabelList);

            return LabelList.Select(p => p.GetLabelDto()).ToList();
        }

        private List<Label> GetAllLabelWithChildren(ParentChildExcelDto parent, List<ParentChildExcelDto> list)
        {
            var result = new List<Label>();
            list.Where(x => x.ParentCode == parent.Code).ToList().ForEach(z =>
            {
                result.Add(new Label(0, z.Title, GetAllLabelWithChildren(z, list)));
            });
            return result;
        }

        private async Task SaveToQuery(List<Label> list)
        {
            var result = new List<LabelQuery>();
            foreach (var p in list)
            {
                await _LabelQueryService.Create(new QueryModel.LabelQuery(p.Id, p.Title, p.ParentId, DateTime.Now, Guid.NewGuid()));
                if (p.SubLabels != null && p.SubLabels.Any())
                {
                    await SaveToQuery(p.SubLabels.ToList());
                }
            }
        }
    }
}