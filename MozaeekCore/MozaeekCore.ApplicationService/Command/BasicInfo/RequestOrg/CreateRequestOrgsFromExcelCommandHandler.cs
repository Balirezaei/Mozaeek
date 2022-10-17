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
    public class CreateRequestOrgsFromExcelCommandHandler : IBaseAsyncCommandHandler<CreateRequestOrgFromExcelCommand, List<RequestOrgDto>>
    {
        private readonly IGenericRepository<Domain.RequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRequestOrgQueryService _RequestOrgQueryService;

        public CreateRequestOrgsFromExcelCommandHandler(IGenericRepository<Domain.RequestOrg> repository, IUnitOfWork unitOfWork, IRequestOrgQueryService RequestOrgQueryService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _RequestOrgQueryService = RequestOrgQueryService;
        }
        public async Task<List<RequestOrgDto>> HandleAsync(CreateRequestOrgFromExcelCommand cmd)
        {
            var result = cmd.ExcelPath.ImportExcel();
            var RequestOrgList = result.Where(r => (string.IsNullOrEmpty(r.ParentCode) || r.ParentCode == "0")).Select(r =>
            {
                var children = GetAllRequestOrgWithChildren(r, result.ToList());

                return new RequestOrg(0, r.Title, children);
            }).ToList();

            await _repository.AddRange(RequestOrgList);
            await _unitOfWork.CommitAsync();

            await SaveToQuery(RequestOrgList);

            return RequestOrgList.Select(p => p.GetRequestOrgDto()).ToList();
        }

        private List<Domain.RequestOrg> GetAllRequestOrgWithChildren(ParentChildExcelDto parent, List<ParentChildExcelDto> list)
        {
            var result = new List<RequestOrg>();
            list.Where(x => x.ParentCode == parent.Code).ToList().ForEach(z =>
            {
                result.Add(new RequestOrg(0, z.Title, GetAllRequestOrgWithChildren(z, list)));
            });
            return result;
        }

        private async Task SaveToQuery(List<RequestOrg> list)
        {
            var result = new List<RequestOrgQuery>();
            foreach (var p in list)
            {
                await _RequestOrgQueryService.Create(new QueryModel.RequestOrgQuery(p.Id, p.Title, "", p.ParentId, DateTime.Now, Guid.NewGuid()));
                if (p.SubRequestOrgs != null && p.SubRequestOrgs.Any())
                {
                    await SaveToQuery(p.SubRequestOrgs.ToList());
                }
            }
        }
    }
}