using Mozaeek.Notification.Domain.IRepository;
using Mozaeek.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mozaeek.Notification.Domain.Statics;

namespace Mozaeek.Notification.Persistence.Repository
{
    public class SmsRepository : ISmsRepository
    {
        private readonly NotificationDbContext dbContext;

        public SmsRepository(NotificationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(SmsMessage smsMessage)
        {
            dbContext.SmsMessages.Add(smsMessage);
            
        }

        public Task<SmsProvider[]> GetProviders()
        {
            return dbContext.SmsProviders.ToArrayAsync();
        }

        public Task<List<SmsMessage>> GetForDeliveryStatusUpdate()
        {
            return dbContext.SmsMessages.Where(x => SmsDeliveryStates.GetNonFinalStatuses().Contains(x.DeliveryStatus)).ToListAsync();
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
