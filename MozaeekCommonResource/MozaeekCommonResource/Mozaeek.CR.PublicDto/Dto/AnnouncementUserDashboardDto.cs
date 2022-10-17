using System;
using System.Collections.Generic;
using System.Text;

namespace Mozaeek.CR.PublicDto.Dto
{
    public class AnnouncementUserDashboardDto
    {
        public long UserId { get; set; }
        public List<long> Subjects { get; set; }

    }
}
