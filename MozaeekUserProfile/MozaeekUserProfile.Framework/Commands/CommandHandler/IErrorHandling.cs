using System;

namespace MozaeekUserProfile.Core.Core.CommandHandler
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}