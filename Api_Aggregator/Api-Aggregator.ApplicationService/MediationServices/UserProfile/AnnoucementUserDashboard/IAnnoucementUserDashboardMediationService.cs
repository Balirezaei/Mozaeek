using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using Mozaeek.CR.PublicDto.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api_Aggregator.ApplicationService.MediationServices.UserProfile.AnnoucementUserDashboard
{
   public interface IAnnoucementUserDashboardMediationService
    {
        Task<AnnouncementUserDashboardDto> GetUserAnnouncement(long userId);
    }
}
