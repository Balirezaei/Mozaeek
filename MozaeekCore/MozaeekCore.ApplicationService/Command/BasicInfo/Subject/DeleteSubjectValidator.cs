using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.Commands;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteSubjectValidator : ICommandValidator<DeleteSubjectCommand>
    {
        private readonly IGenericRepository<Subject> _repository;

        public DeleteSubjectValidator(IGenericRepository<Subject> _repository)
        {
            this._repository = _repository;
        }

        public async ValueTask ValidateAsync(DeleteSubjectCommand command)
        {
            if (await _repository.GetByPredicate(m => m.ParentId == command.Id).AnyAsync())
            {
                throw new UserFriendlyException("امکان حذف با وجود زیر مجموعه فراهم نمی باشد.");
            }
        }
    }
}