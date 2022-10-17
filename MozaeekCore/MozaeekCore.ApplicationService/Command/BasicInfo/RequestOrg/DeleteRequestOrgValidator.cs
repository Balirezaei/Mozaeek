using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Core.Commands;
using MozaeekCore.Domain;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestOrgValidator : ICommandValidator<DeleteRequestOrgCommand>
    {
        private readonly IGenericRepository<RequestOrg> _repository;

        public DeleteRequestOrgValidator(IGenericRepository<RequestOrg> _repository)
        {
            this._repository = _repository;
        }

        public async ValueTask ValidateAsync(DeleteRequestOrgCommand command)
        {
            if (await _repository.GetByPredicate(m => m.ParentId == command.Id).AnyAsync())
            {
                throw new UserFriendlyException("امکان حذف با وجود زیرمجموعه فراهم نمی باشد.");
            }
        }
    }
}