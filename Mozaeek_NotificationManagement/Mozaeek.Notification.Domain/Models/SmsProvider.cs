using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.Notification.Domain.Models
{
    public class SmsProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SenderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
