using System.Collections.Generic;
using System.Linq;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.QueryModel;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Mapper.Announcement
{
    public static class AnnouncementProfile
    {
        public static NewsForProcessGrid GetNewForProcessGrid(this RSSNews domain)
        {
            return new NewsForProcessGrid()
            {
                Link = domain.Link,
                Title = domain.Title,
                CreateDate = domain.CreateDate.GetTimeFromNow(),
                Id = domain.Id,
                Source = domain.Source
            };
        }


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
                Labels =
                    domain.LabelList
                        .Select(z => z.Title).ToList(),
                Subjects = domain.SubjectList.Select(z => z.Title).ToList(),
                RequestOrgs = domain.RequestOrgList.Select(z => z.Title).ToList(),
                Points = domain.PointList.Select(z => z.Title).ToList(),
                PublishDate = domain.ReleaseDate.GetTimeFromNow()
            };
        }
        public static AnnouncementRequestGrid GetAnnouncementRequestGrid(this AnnouncementQuery domain)
        {
            return new AnnouncementRequestGrid()
            {
                Id = domain.Id,
                Title = domain.Title,
                Labels =
                    domain.LabelList
                        .Select(z => z.Title).ToList(),
                Subjects = domain.SubjectList.Select(z => z.Title).ToList(),
                RequestOrgs = domain.RequestOrgList.Select(z => z.Title).ToList(),
                Points = domain.PointList.Select(z => z.Title).ToList(),
                PublishDate = domain.ReleaseDate.GetTimeFromNow()
            };
        }

        public static AnnouncementDto GetAnnouncement(this AnnouncementQuery domain)
        {
            var dto = new AnnouncementDto()
            {
                Title = domain.Title,
                Summary = domain.Summary,
                Description = domain.Description,
                Subjects = domain.SubjectList.Select(m=>new SubjectDto(){Title = m.Title,Id = m.Id}).ToList(),
                Labels = domain.LabelList.Select(m=>new LabelDto(){Title = m.Title,Id = m.Id}).ToList(),
                RequestOrgs = domain.RequestOrgList.Select(m=>new RequestOrgDto(){Title = m.Title,Id = m.Id}).ToList(),
                Points = domain.PointList.Select(m => m.Id).ToList(),
                ImagePath = domain.ImageUrl,
                Id = domain.Id,
                HasRequest = domain.HasRequest
            };
            return dto;
        }

        public static UserAnnouncementDto GetUserAnnouncementDto(this AnnouncementQuery domain)
        {
            return new UserAnnouncementDto()
            {
                Title = domain.Title,
                PictureUrl = domain.ImageUrl,
                CreateDateTime = domain.ReleaseDate.GetTimeFromNow(),
                Id = domain.Id
            };
        }

        public static SingleUserAnnouncementDto GetSingleUserAnnouncement(this AnnouncementDto dto)
        {
            return new SingleUserAnnouncementDto()
            {
                Title = dto.Title,
                Description = dto.Description,
                Id = dto.Id
            };
        }

        public static RequestTargetMobileView GetRequestTargetMobile(this RequestTargetQuery dto)
        {
            return new RequestTargetMobileView()
            {
                Title = dto.Title,
                Id = dto.Id
            };
        }
    }
}