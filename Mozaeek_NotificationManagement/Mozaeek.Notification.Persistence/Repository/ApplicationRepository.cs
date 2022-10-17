using Mozaeek.Notification.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mozaeek.Notification.Persistence.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly NotificationDbContext dbContext;
        public ApplicationRepository( IServiceProvider serviceProvider)
        {
            this.dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<NotificationDbContext>();
        }
        public async Task<string> GetAppId(int id)
        {
            return (await dbContext.Application.SingleOrDefaultAsync(x => x.Id == id)).AppId;
        }
    }
}
