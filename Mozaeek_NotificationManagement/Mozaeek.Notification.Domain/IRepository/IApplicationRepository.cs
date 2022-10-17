using Mozaeek.Notification.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Domain.IRepository
{
    public interface IApplicationRepository
    {
        Task<string> GetAppId(int id);
    }
}
