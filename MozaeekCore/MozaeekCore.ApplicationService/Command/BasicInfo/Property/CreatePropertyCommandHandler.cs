using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreatePropertyCommandHandler : IBaseAsyncCommandHandler<CreatePropertyCommand, CreatePropertyCommandResult>
    {
        private readonly IGenericRepository<Property> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreatePropertyCommandHandler(IGenericRepository<Property> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreatePropertyCommandResult> HandleAsync(CreatePropertyCommand cmd)
        {
            var property = new Property(cmd.Title, cmd.Description, cmd.PropertyType, cmd.PropertyDataType);
            await _repository.Add(property);


            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new PropertyCreatedOrUpdated(property.Id, property.Title, property.Description, property.PropertyType, property.PropertyDataType, true));


            return new CreatePropertyCommandResult()
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