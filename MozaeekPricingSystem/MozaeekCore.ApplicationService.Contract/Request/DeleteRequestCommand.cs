using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRequestCommand : Command
    {
        public DeleteRequestCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}