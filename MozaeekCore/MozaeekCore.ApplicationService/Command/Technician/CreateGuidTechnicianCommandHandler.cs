using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Technician;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Technician;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateGuidTechnicianCommandHandler : IBaseAsyncCommandHandler<CreateGuidTechnicianCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _fileRepository;
        private readonly IMessagePublisher _publisher;
        public CreateGuidTechnicianCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork, IMessagePublisher publisher, IFileRepository fileRepository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _publisher = publisher;
            _fileRepository = fileRepository;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(CreateGuidTechnicianCommand cmd)
        {
            var technicianFiles = cmd.FileIds.Select(f => new Domain.TechnicianAttachment() { FileId = f }).ToList();
            var technicianSubjects = cmd.SubjectIds.Select(s => new TechnicianSubject() { SubjectId = s}).ToList();
            
            var technician = new Technician(0, cmd.PhoneNmuber, cmd.FirstName, cmd.LastName,"", Mozaeek.CR.PublicDto.TechnicianType.Guide, cmd.NationalId, "", "", null, null, technicianSubjects, null, technicianFiles);
            _repository.Add(technician);
            await _unitOfWork.CommitAsync();
            var attachments = new List<Domain.Contract.Technician.TechnicianAttachment>();
            cmd.FileIds.ForEach(f =>
            {
                var file = _fileRepository.Find(f).Result;
                attachments.Add(new Domain.Contract.Technician.TechnicianAttachment() { FileHttpAddress = file.Url, FileId = file.Id, FileName = file.Name });
            });

            await _publisher.PublishAsync(new TechnicianCreatedOrUpdated(technician.Id, technician.PhoneNumber, technician.FirstName, technician.LastName, technician.Email, technician.Address, technician.NationalId, technician.PostalCode, technician.PointId, Mozaeek.CR.PublicDto.TechnicianType.Expert, null, null, cmd.SubjectIds, attachments, false, false, technician.CreateTime, true));

            return new RegisterTechnicianCommandResult()
            {
                Id = technician.Id
            };

        }
    }
}
