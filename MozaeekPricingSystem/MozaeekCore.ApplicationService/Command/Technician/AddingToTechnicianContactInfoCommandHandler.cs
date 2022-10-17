using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class AddingToTechnicianContactInfoCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianContactInfoCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public AddingToTechnicianContactInfoCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianContactInfoCommand cmd)
        {
            var technician = await _repository.FindWithContactInfo(cmd.TechnicianId);
            var contactInfo = new TechnicianContactInfo(cmd.MobileNumber, cmd.PhoneNumber, cmd.OfficeNumber, cmd.PostalCode, cmd.Address);
            technician.AppendContactInfo(contactInfo);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new TechnicianContactInfoAdded(technician.Id, contactInfo.MobileNumber,
                contactInfo.PhoneNumber, contactInfo.OfficeNumber, contactInfo.PostalCode, contactInfo.Address));

            return new RegisterTechnicianCommandResult()
            {
                Id = cmd.TechnicianId
            };

        }
    }
}