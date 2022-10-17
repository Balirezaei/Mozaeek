using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Domain.Dtos
{
    public class SmsMessageDto: MessageDto
    {
        public string MobileNo { get; set; }
    }
}
