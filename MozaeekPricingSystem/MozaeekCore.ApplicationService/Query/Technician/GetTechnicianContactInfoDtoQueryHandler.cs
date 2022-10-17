using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetTechnicianContactInfoDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianContactInfoDto>
    {
        private readonly ITechnicianRepository repository;

        public GetTechnicianContactInfoDtoQueryHandler(ITechnicianRepository repository)
        {
            this.repository = repository;
        }
        public async Task<TechnicianContactInfoDto> HandleAsync(FindByKey query)
        {
            var res = await repository.FindWithContactInfo(query.Id);
            var contactInfo = new TechnicianContactInfoDto()
            {
                TechnicianId = res.Id,

            };

            if (res.TechnicianContactInfo != null)
            {
                contactInfo.MobileNumber = res.TechnicianContactInfo.MobileNumber;
                contactInfo.PhoneNumber = res.TechnicianContactInfo.PhoneNumber;
                contactInfo.OfficeNumber = res.TechnicianContactInfo.OfficeNumber;
                contactInfo.Address = res.TechnicianContactInfo.Address;
                contactInfo.PostalCode = res.TechnicianContactInfo.PostalCode;
            }

            return contactInfo;
        }
    }
}