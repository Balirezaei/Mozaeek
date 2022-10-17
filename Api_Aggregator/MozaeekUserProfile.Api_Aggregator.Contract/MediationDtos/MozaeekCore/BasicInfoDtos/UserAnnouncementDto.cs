using System;
using System.Collections.Generic;
using System.Text;
using Api_Aggregator.Infrastructure.ResponseMessages;

namespace Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos
{
    /// <summary>
    /// اعلانیه ها جهت نمایش در اپ کاربران
    /// </summary>
    public class UserAnnouncementDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CreateDateTime { get; set; }
        public string PictureUrl { get; set; }
    }


    // public class UserDashboardAnnoucementResult
    // {
    //     public Data data { get; set; }
    //     public object error { get; set; }
    // }

    public class Data
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public int totalCount { get; set; }
        public UserAnnouncementDto[] List { get; set; }
    }
    public class UserSearchResult
    {
        public PagedListResult<UserAnnouncementDto> Announcments { get; set; }
        public PagedListResult<RequestTargetMobileView> RequestTargets { get; set; }
    }

    public class RequestTargetMobileView
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
    public class CharacteristicUserDashboardDto
    {
        public long SelectedLabelId { get; set; }
        public long ParentLabelId { get; set; }
        public string SelectedLabelTitle { get; set; }
    }
    public class FullUserSearchByUserCharacteristics
    {
        public List<long> LabelIds { get; set; }
    }
    // public class List
    // {
    //     public string title { get; set; }
    //     public DateTime createDateTime { get; set; }
    //     public object picture { get; set; }
    //     public object pictureType { get; set; }
    // }
}
