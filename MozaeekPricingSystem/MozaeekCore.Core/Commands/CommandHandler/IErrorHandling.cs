using System;

namespace MozaeekCore.Core.CommandHandler
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}