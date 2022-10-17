namespace Mozaeek.Notification.BackgroundJob.Configurations
{
    public interface ILogger
    {
        void DoLog(string message);

        void DoLogInsideApp(string message);
    }
}
