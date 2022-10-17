using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRequestOrgCommand : Command
    {
        public DeleteRequestOrgCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}