using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRequestActCommand : Command
    {
        public DeleteRequestActCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}