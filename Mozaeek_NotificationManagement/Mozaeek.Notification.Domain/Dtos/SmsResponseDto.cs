using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Domain.Dtos
{
    public class SmsResponseDto
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int DeliveryCode { get; set; }
    }
}
