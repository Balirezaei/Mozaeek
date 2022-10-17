using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.Commands;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeletePointValidator : ICommandValidator<DeletePointCommand>
    {
        private readonly IGenericRepository<Point> _repository;

        public DeletePointValidator(IGenericRepository<Point> _repository)
        {
            this._repository = _repository;
        }

        public async ValueTask ValidateAsync(DeletePointCommand command)
        {
            if (await _repository.GetByPredicate(m => m.ParentId == command.Id).AnyAsync())
            {
                throw new UserFriendlyException("امکان حذف با وجود زیرمجموعه فراهم نمی باشد.");
            }
        }
    }
}