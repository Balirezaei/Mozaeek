using System;
using Mozaeek.CR.PublicDto;

namespace MozaeekUserProfile.Domain
{
    public class QuestionState
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string SerializedEvent { get; set; }
        public UserQuestionState UserQuestionState { get; set; }
        public UserQuestion UserQuestion { get; set; }
        public long UserQuestionId { get; set; }
    }
}