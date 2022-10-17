using System.Threading.Tasks;
using Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.BasicInfo;
using Api_Aggregator.Contract.MediationDtos;

namespace Api_Aggregator.ApplicationService.AggregationServices
{
    public interface IUserProfileSubjectPriceService
    {
        Task<SubjectWithPriceDetailDto> GetSubjectPriceDto(long subjectId);
    }

    public class UserProfileSubjectPriceService : IUserProfileSubjectPriceService
    {
        private readonly IBasicInfoMediationService _basicInfoMediationService;

        public UserProfileSubjectPriceService(IBasicInfoMediationService basicInfoMediationService)
        {
            _basicInfoMediationService = basicInfoMediationService;
        }

        public Task<SubjectWithPriceDetailDto> GetSubjectPriceDto(long subjectId)
        {
            return _basicInfoMediationService.GetSubjectWithPriceDetail(subjectId);
        }
    }
}