using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class PreRequestDeleteCommand : Command
    {
        public PreRequestDeleteCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}