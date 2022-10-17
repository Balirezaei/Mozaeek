using System;
using System.Threading.Tasks;
using MozaeekCore.Core.CommandHandler;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MozaeekCore.LogManagement
{
    public class SerilogErrorHandle : IErrorHandling
    {
        public Task HandleException(Exception exception)
        {
            var stackTrace = exception.StackTrace;
            var message = exception.Message;
                Serilog.Log.Information(exception, "SailorLogError");
            return Task.CompletedTask;
        }
    }
}