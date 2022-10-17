using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.Commands;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateTechnicianPersonalInfoCommandValidator : ICommandValidator<CreateTechnicianPersonalInfoCommand>
    {
        public ValueTask ValidateAsync(CreateTechnicianPersonalInfoCommand command)
        {
            if ((int)command.TechnicianType == 0)
            {
                throw new UserFriendlyException("انتخاب نوع کاردان الزامیست.");
            }
            return default;
        }
    }
}