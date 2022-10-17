using System;

namespace MozaeekTechnicianProfile.Core.CommandHandler
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}