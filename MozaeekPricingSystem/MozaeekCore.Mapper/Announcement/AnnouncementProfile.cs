using System.Collections.Generic;
using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.QueryModel;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Mapper.Announcement
{
    public static class AnnouncementProfile
    {
        public static NewsForProcess GetNewForProcess(this RSSNews domain)
        {
            return new NewsForProcess()
            {
                Link = domain.Link,
                Title = domain.Title,
                Description = domain.Description,
                CreateDate = domain.CreateDate,
                Id = domain.Id,
                Source = domain.Source
            };
        }
        public static AnnouncementGrid GetAnnouncementGrid(this AnnouncementQuery domain)
        {
            return new AnnouncementGrid()
            {
                Id = domain.Id,
                Title = domain.Title,
                RequestTargetRequestOrgs =
                    domain.RequestTarget.RequestOrgList
                        .Select(z => z.Title).ToList(),
                RequestTargetSubjects = domain.RequestTarget.SubjectList.Select(z =>z.Title).ToList(),
                RequestTargetLabels = domain.RequestTarget.LabelList.Select(z => z.Title).ToList(),
                Points = domain.PointList.Select(z =>z.Title).ToList()

            };
        }
        public static AnnouncementDto GetAnnouncement(this Domain.Announcement domain)
        {
            return new AnnouncementDto()
            {
                Title = domain.Title,
                Description = domain.Description,
                RequestTargetTitle = domain.RequestTarget.Title,
                Points = domain.AnnouncementPoints.Select(m => m.PointId).ToList(),
            };
        }

    }
}