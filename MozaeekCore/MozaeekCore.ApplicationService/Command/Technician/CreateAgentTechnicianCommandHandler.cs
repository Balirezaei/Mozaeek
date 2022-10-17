using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Technician;
using MozaeekCore.Core;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain.Contract.Technician;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateAgentTechnicianCommandHandler : IBaseAsyncCommandHandler<CreateAgentTechnicianCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly IFileRepository _fileRepository;
        public CreateAgentTechnicianCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher, IFileRepository fileRepository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
            _fileRepository = fileRepository;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(CreateAgentTechnicianCommand cmd)
        {
            var technicianFiles = cmd.FileIds.Select(f => new Domain.TechnicianAttachment() { FileId = f }).ToList();
            var technician = new Technician(0,cmd.PhoneNmuber,cmd.FirstName,cmd.LastName,cmd.Email,Mozaeek.CR.PublicDto.TechnicianType.Agent,cmd.NationalId,cmd.Address,cmd.PostalCode,cmd.PointId,null,null,null,technicianFiles  );
            _repository.Add(technician);
            await _unitOfWork.CommitAsync();
            var attachments = new List<Domain.Contract.Technician.TechnicianAttachment>();
            cmd.FileIds.ForEach( f=> 
            {
                var file =  _fileRepository.Find(f).Result;
                attachments.Add(new Domain.Contract.Technician.TechnicianAttachment() {FileHttpAddress=file.Url,FileId=file.Id,FileName=file.Name });
            });

            await _publisher.PublishAsync(new TechnicianCreatedOrUpdated(technician.Id, technician.PhoneNumber, technician.FirstName, technician.LastName, technician.Email, technician.Address, technician.NationalId, technician.PostalCode, technician.PointId, Mozaeek.CR.PublicDto.TechnicianType.Agent, null, null, null, attachments, false, false, technician.CreateTime,true));
            return new RegisterTechnicianCommandResult()
            {
                Id = technician.Id
            };

        }
    }
}
