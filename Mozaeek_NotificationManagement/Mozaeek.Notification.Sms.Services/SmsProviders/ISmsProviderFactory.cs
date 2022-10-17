namespace Mozaeek.Notification.Sms.Services.SmsProviders
{
    public interface ISmsProviderFactory
    {
        ISmsProvider GetCurrentProvider();
        ISmsProvider GetByKey(string key);
    }
}
