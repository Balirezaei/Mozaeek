namespace MozaeekCore.OutboxPublisherService.Service
{
    public interface ILogger
    {
        void DoLog(string message);

        void DoLogInsideApp(string message);
    }
}