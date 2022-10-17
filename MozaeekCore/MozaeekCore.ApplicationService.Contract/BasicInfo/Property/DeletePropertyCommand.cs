using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeletePropertyCommand : Command
    {
        public DeletePropertyCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}