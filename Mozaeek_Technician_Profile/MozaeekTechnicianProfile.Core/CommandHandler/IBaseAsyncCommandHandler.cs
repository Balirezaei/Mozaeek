using MozaeekTechnicianProfile.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekTechnicianProfile.Core.CommandHandler
{
    public interface IBaseAsyncCommandHandler<T, TResult> where T : Command
    {
        Task<TResult> HandleAsync(T cmd);
    }
}
