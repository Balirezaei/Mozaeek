using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRSSCommand : Command
    {
        public DeleteRSSCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}