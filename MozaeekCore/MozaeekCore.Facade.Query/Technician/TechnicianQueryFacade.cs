using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public class TechnicianQueryFacade: ITechnicianQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public TechnicianQueryFacade(IQueryProcessor queryProcessor)
        {
            this._queryProcessor = queryProcessor;
        }
        public Task<TechnicianDto> GetTechnicianById(int id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianDto>(new FindByKey(id));
        }

        public Task<InitTechnicianEducationalInfo> GetInitTechnicianEducationalInfo()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitTechnicianEducationalInfo>(new Nothing());
        }

        public Task<TechnicianEducationalInfoDto> GetTechnicianEducationalInfoByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianEducationalInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianContactInfoDto> GetTechnicianContactInfoByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianContactInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianPersonalInfoDto> GetTechnicianPersonalInfoByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianPersonalInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianSubjectDto> GetTechnicianSubjectByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianSubjectDto>(new FindByKey(id));
        }

        public Task<TechnicianPointDto> GetTechnicianPointByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianPointDto>(new FindByKey(id));
        }

        public Task<TechnicianRequestDto> GetTechnicianRequestByTechnicianId(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, TechnicianRequestDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<TechnicianDto>> GetAll()
        {
            var  list=await _queryProcessor.ProcessAsync<Nothing, List<TechnicianDto>>(new Nothing());
            return new PagedListResult<TechnicianDto>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1,
            };
        }

        //public async Task<PagedListResult<TechnicianGridDto>> TechnicianGrid(PagingContract pagingContract)
        //{
        //    var list = await _queryProcessor.ProcessAsync<PagingContract, List<TechnicianGridDto>>(pagingContract);
        //    var count = await _queryProcessor.ProcessAsync<Nothing, RequestTotalCount>(new Nothing());

        //    return new PagedListResult<TechnicianGridDto>()
        //    {
        //        List = list,
        //        TotalCount = count.Count,
        //        PageNumber = pagingContract.PageNumber,
        //        PageSize = pagingContract.PageSize,
        //    };
        //}
    }
}
