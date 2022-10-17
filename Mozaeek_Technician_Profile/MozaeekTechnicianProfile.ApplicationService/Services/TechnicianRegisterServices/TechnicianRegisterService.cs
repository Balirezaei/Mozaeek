using System.Threading.Tasks;
using MozaeekTechnicianProfile.ApplicationService.Contract.Dtos;
using MozaeekTechnicianProfile.Core.Core;
using MozaeekTechnicianProfile.Domain;
using System.Linq;
namespace MozaeekTechnicianProfile.ApplicationService.Services.TechnicianProfileServices
{
    public class TechnicianRegisterService : ITechnicianRegisterService
    {
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TechnicianRegisterService(ITechnicianRepository technicianRepository, IUnitOfWork unitOfWork)
        {
            this._technicianRepository = technicianRepository;
            _unitOfWork = unitOfWork;
        }
        //public async Task CompleteProfile(TechnicianProfileCompleteInputeDto inputeDto)
        //{
        //    // var user = await _technicianRepository.GetUserById(inputeDto.UserId);
        //    // user.UpdateInformation(inputeDto.FirstName, inputeDto.LastName, inputeDto.Email, inputeDto.Address);
        //    // _technicianRepository.UpdateProfile(user);
        //    // await _unitOfWork.CommitAsync();
        //}

        public async Task<int> CreateProfile(CreateTechnicianProfileDto input)
        {
            var technicianOfflineRequests = input.TechnicianOflineRequests?.Select(t => new Domain.TechnicianOflineRequest { Id = t.Id, RequestTitle = t.RequestTitle, RequestId = t.RequestId }).ToList();
            var technicianSubject = input.TechnicianSubjects?.Select(t => new Domain.TechnicianSubject { Id = t.Id, SubjectTitle = t.SubjectTitle, SubjectId = t.SubjectId }).ToList();
            var technicianAttachement = input.TechnicianAttachements?.Select(t => new Domain.TechnicianAttachement { Id = t.Id, FileName = t.FileName, CoreFileId = t.CoreFileId, Type = t.Type, Url = t.Url }).ToList();
            var technicianDefiniteRequestOrg = input.TechnicianDefiniteRequestOrgs?.Select(t => new Domain.TechnicianDefiniteRequestOrg { Id = t.Id, RequestOrgTitle = t.RequestOrgTitle, RequestOrgId = t.RequestOrgId }).ToList();
            await _technicianRepository.Create(new Technician(0, input.PhoneNumber, input.FirstName, input.LastName, input.Email, input.TechnicianType, input.NationalNumber, input.Address, input.LastRefreshToken, input.PointId, technicianOfflineRequests, technicianSubject, technicianDefiniteRequestOrg, technicianAttachement));
            await _unitOfWork.CommitAsync();
            return 0;
        }

        public async Task<bool> UpdateTechnicianContactInfo(UpdateTechnicinaContactInfoDto input)
        {
            var technician = await _technicianRepository.GetTechnicianById(input.TechnicianId);
            if (!string.IsNullOrEmpty(input.FirstName))
                technician.UpdateTechnicianFirstName(input.FirstName);

            if (!string.IsNullOrEmpty(input.LastName))
                technician.UpdateTechnicianLastName(input.LastName);

            if (!string.IsNullOrEmpty(input.Address))
                technician.UpdateTechnicianAddress(input.Address);

            if (!string.IsNullOrEmpty(input.Email))
                technician.UpdateTechnicianEmail(input.Email);

            if (!string.IsNullOrEmpty(input.IntroducerCode))
                technician.UpdateTechnicianIntroducerCode(input.IntroducerCode);

            if (!string.IsNullOrEmpty(input.MobileNumber))
                technician.UpdateTechnicianPhoneNumber(input.MobileNumber);

            if (!string.IsNullOrEmpty(input.NationalId))
                technician.UpdateTechnicianNationalId(input.NationalId);


            await _unitOfWork.CommitAsync();
            return true;
            
        }

        // public async Task<TechnicianProfileOutputDto> GetCurrentTechnicianProfile(long userId)
        // {
        //     var user = await _technicianRepository.GetUserById(userId);
        //     return user.GetUserInput();
        // }

    }
}
