using Mozaeek.Notification.Domain.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mozaeek.Notification.Domain.Entity
{
    public class Application
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AppId { get; set; }
        public ApplicationTypeEnum Type { get; set; }
    }
}
