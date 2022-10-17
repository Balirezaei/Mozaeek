using MozaeekCore.ApplicationService.Contract.Technician;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateTechnicianVerficationStepCommandHandler : IBaseAsyncCommandHandler<UpdateTechnicianVerificationStepCommand, UpdateTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITechnicianQueryService _mongoQuery;
        
        public UpdateTechnicianVerficationStepCommandHandler(ITechnicianRepository repository, IUnitOfWork unitOfWork, ITechnicianQueryService mongoQuery)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mongoQuery = mongoQuery;
        }

        public async Task<UpdateTechnicianCommandResult> HandleAsync(UpdateTechnicianVerificationStepCommand cmd)
        {
      
            var technician = await _repository.Find(cmd.Id);
            if (cmd.isFirstStepVerified != null)
                technician.FirstVerification = cmd.isFirstStepVerified.Value;
            if (cmd.isSecondStepVefied != null)
                technician.SecondVerification = cmd.isSecondStepVefied.Value;


            _repository.Update(technician);

            await _unitOfWork.CommitAsync();
           await _mongoQuery.UpdateTechnicianVerificationStep(technician.Id, cmd.isFirstStepVerified, cmd.isSecondStepVefied);
            return new UpdateTechnicianCommandResult();

        }
    }
}
