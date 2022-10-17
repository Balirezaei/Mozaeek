using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.Facade.Query
{
    public class TechnicianQueryFacade: ITechnicianQueryFacade
    {
        private readonly IQueryProcessor queryProcessor;

        public TechnicianQueryFacade(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }
        public Task<TechnicianDto> GetTechnicianById(int id)
        {
            return queryProcessor.ProcessAsync<FindByKey, TechnicianDto>(new FindByKey(id));
        }

        public Task<InitTechnicianEducationalInfo> GetInitTechnicianEducationalInfo()
        {
            return queryProcessor.ProcessAsync<Nothing, InitTechnicianEducationalInfo>(new Nothing());
        }

        public Task<TechnicianEducationalInfoDto> GetTechnicianEducationalInfoByTechnicianId(long id)
        {
            return queryProcessor.ProcessAsync<FindByKey, TechnicianEducationalInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianContactInfoDto> GetTechnicianContactInfoByTechnicianId(long id)
        {
            return queryProcessor.ProcessAsync<FindByKey, TechnicianContactInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianPersonalInfoDto> GetTechnicianPersonalInfoByTechnicianId(long id)
        {
            return queryProcessor.ProcessAsync<FindByKey, TechnicianPersonalInfoDto>(new FindByKey(id));
        }

        public Task<TechnicianSubjectDto> GetTechnicianSubjectByTechnicianId(long id)
        {
            return queryProcessor.ProcessAsync<FindByKey, TechnicianSubjectDto>(new FindByKey(id));
        }

        public Task<TechnicianPointDto> GetTechnicianPointByTechnicianId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TechnicianRequestDto> GetTechnicianRequestByTechnicianId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
