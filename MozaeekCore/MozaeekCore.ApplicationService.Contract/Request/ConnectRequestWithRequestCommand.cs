using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class ConnectRequestWithRequestCommand : Command
    {
        public long RequestId { get; set; }
        public long ConnectedRequestId { get; set; }
    }
    public class RemoveConnectedRequestWithRequestCommand : Command
    {
        public long RequestId { get; set; }
        public long ConnectedRequestId { get; set; }
    }
    public class ConnectRequestWithRequestResult
    {
        public long RequestId { get; set; }
        public long ConnectedRequestId { get; set; }
    }

    public class ConnectedRequestDto
    {
        public long ConnectedRequestId { get; set; }
        public string Title { get; set; }
    }
}