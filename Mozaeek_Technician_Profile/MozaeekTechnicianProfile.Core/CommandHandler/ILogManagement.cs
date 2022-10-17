namespace MozaeekTechnicianProfile.Core.CommandHandler
{
    public interface ILogManagement
    {
        void DoLog<T>(T command);
    }
}