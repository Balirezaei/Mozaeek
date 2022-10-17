using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mozaeek.Notification.Domain.Models
{
    public class SmsMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public bool IsDelivered { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public string Recievier { get; set; }
        public string CorrelationId { get; set; }
        public int? StatusCode { get; set; }
        public string ProviderName { get; set; }
        public int DeliveryStatus { get; set; }
        public int DeliveryCode { get; set; }
    }
}
