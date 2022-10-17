namespace MozaeekUserProfile.Core.Core.CommandHandler
{
    public interface ILogManagement
    {
        void DoLog<T>(T command);
    }
}