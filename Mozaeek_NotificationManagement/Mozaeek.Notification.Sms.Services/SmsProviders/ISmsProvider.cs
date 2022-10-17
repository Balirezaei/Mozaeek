using Mozaeek.Notification.Domain.Dtos;
using Mozaeek.Notification.Domain.Models;
using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mozaeek.Notification.Sms.Services.SmsProviders
{
    public interface ISmsProvider
    {
        string Key { get; }
        Task<SmsResponseDto> Send(string recipient, string message);
        Task<SmsResponseDto> SendPattern(string recipient, string message);
        Task<SmsResponseDto> Send(string[] recipients, string message);
        Task<DeliveryStatus[]> GetDelivery(int deliveryCode);
        
    }
}
