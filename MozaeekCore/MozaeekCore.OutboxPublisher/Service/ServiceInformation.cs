namespace MozaeekCore.OutboxPublisherService.Service
{
    public class ServiceInformation
    {
        public ServiceInformation(string serviceName)
        {
            ServiceName = serviceName;
        }

        public string ServiceName { get; set; }
    }
}