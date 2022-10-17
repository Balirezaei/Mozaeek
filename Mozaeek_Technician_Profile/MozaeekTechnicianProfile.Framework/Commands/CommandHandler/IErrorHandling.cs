using System;

namespace MozaeekTechnicianProfile.Core.Core.CommandHandler
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}