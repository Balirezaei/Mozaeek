using Mozaeek.Notification.Domain.Entity;
using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mozaeek.Notification.Persistence.Repository
{
    public class PushNotificationRepository : IPushNotificationRepository
    {
        private readonly NotificationDbContext dbContext;

        public PushNotificationRepository(IServiceProvider serviceProvider)
        {
            this.dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<NotificationDbContext>();
        }

        public async Task<List<PushNotification>> GetPushNotificationsByStatus(DeliveryStatusEnum deliveryStatus)
        {
            return await dbContext.PushNotification.Where(x => x.DeliveryStatus == deliveryStatus).ToListAsync();
        }

        public async Task InsertAsync(PushNotification pushNotification)
        {
            await dbContext.PushNotification.AddAsync(pushNotification);
            await dbContext.SaveChangesAsync();
        }

        public void UpdateDeliveryStatus(long id, DeliveryStatusEnum status)
        {
            var entity =  dbContext.PushNotification.SingleOrDefault(x => x.Id == id);
            entity.DeliveryStatus = status;
            dbContext.SaveChanges();

        }

        public async Task UpdateNotificationWrapperId(long id, string wrapperId)
        {
            var entity = await dbContext.PushNotification.SingleOrDefaultAsync(x => x.Id == id);
            entity.NotificationWrapperId = wrapperId;
            await dbContext.SaveChangesAsync();
        }

 
    }
}
