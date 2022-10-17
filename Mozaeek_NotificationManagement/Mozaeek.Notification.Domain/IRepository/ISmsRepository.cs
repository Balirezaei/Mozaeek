using Mozaeek.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Domain.IRepository
{
    public interface ISmsRepository
    {
        void Add(SmsMessage smsMessage);
        Task SaveAsync();
        Task<SmsProvider[]> GetProviders();
        Task<List<SmsMessage>> GetForDeliveryStatusUpdate();
    }
}
