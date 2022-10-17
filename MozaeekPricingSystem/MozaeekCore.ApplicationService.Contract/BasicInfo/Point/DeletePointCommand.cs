using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeletePointCommand : Command
    {
        public DeletePointCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}