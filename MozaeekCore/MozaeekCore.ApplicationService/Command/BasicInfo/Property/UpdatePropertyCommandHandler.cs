using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdatePropertyCommandHandler : IBaseAsyncCommandHandler<UpdatePropertyCommand, UpdatePropertyCommandResult>
    {
        private readonly IGenericRepository<Property> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdatePropertyCommandHandler(IGenericRepository<Property> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdatePropertyCommandResult> HandleAsync(UpdatePropertyCommand cmd)
        {
            var property = await _repository.Find(cmd.Id);
            property.Update(cmd.Title,cmd.Description,cmd.PropertyType,cmd.PropertyDataType);
            _repository.Update(property);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PropertyCreatedOrUpdated(property.Id, property.Title, property.Description, property.PropertyType, property.PropertyDataType, false));

            return new UpdatePropertyCommandResult()
            {
                Id = property.Id,
                Description = property.Description,
                PropertyDataType = property.PropertyDataType,
                PropertyType = property.PropertyType,
                Title = property.Title
            };
        }
    }
}