using System;

namespace MozaeekUserProfile.Core.CommandHandler
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}