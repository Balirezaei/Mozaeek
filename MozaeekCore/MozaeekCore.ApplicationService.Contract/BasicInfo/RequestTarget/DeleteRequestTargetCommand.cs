using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRequestTargetCommand : Command
    {
        public DeleteRequestTargetCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}