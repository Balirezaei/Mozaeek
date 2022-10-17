using Api_Aggregator.Contract.MediationDtos.MozaeekCore.BasicInfoDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api_Aggregator.Contract.MediationDtos
{

    // public class UserAnnoucementResult
    // {
    //     public AnnouncementUserDashboardDto data { get; set; }
    //     public object error { get; set; }
    // }
    public class CreateQuestionResult
    {
        public long QuestionId { get; set; }
        public string QuestionCode { get; set; }
    }



}
