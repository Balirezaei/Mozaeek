using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.Commands;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteLabelValidator : ICommandValidator<DeleteLabelCommand>
    {
        private readonly IGenericRepository<Label> _repository;

        public DeleteLabelValidator(IGenericRepository<Label> _repository)
        {
            this._repository = _repository;
        }

        public async ValueTask ValidateAsync(DeleteLabelCommand command)
        {
            if (await _repository.GetByPredicate(m => m.ParentId == command.Id).AnyAsync())
            {
                throw new UserFriendlyException("امکان حذف با وجود زیرمجموعه فراهم نمی باشد.");
            }
        }
    }
}