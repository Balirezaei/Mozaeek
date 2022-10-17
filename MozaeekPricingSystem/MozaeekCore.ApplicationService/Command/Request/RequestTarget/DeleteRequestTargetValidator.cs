using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.Commands;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestTargetValidator : ICommandValidator<DeleteRequestTargetCommand>
    {
        private readonly IRequestTargetRepository _repository;

        public DeleteRequestTargetValidator(IRequestTargetRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask ValidateAsync(DeleteRequestTargetCommand command)
        {
            if (!await _repository.CanBeDeletedByAnouncement(command.Id))
            {
                throw new UserFriendlyException("امکان حذف مراد با وجود اعلانات ثبت شده وجود ندارد.");
            }

            if (!await _repository.CanBeDeletedByRequest(command.Id))
            {
                throw new UserFriendlyException("امکان حذف مراد با وجود خواست ثبت شده وجود ندارد.");
            }
        }
    }
}