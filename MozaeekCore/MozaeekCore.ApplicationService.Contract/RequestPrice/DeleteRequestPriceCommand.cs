using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteRequestPriceCommand : Command
    {
        public DeleteRequestPriceCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}