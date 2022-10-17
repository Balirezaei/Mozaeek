using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteSubjectPriceCommand : Command
    {
        public DeleteSubjectPriceCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}