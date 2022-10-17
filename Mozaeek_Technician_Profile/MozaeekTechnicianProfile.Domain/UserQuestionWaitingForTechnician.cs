using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Enum;

namespace MozaeekTechnicianProfile.Domain
{
    public class UserQuestionWaitingForTechnician
    {
        public long Id { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionTitle { get; set; }
        public string UserFullName { get; set; }
        public string UserDeviceId { get; set; }
        public long UserId { get; set; }
        public long QuestionId { get; set; }
        public string QuestionTextDescription { get; set; }
        public string VoiceHttpPath { get; set; }
        public long? VoiceFileId { get; set; }

        public QuestionAnswerType QuestionType { get; set; }
        public QuestionAnswerType AnswerType { get; set; }

        public int UnitPrice { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public int TechnicianPriceShare { get; set; }
        public int SystemPriceShare { get; set; }
        public PriceCurrencyType PriceCurrencyType { get; set; }
        public UserQuestionState UserQuestionState { get; set; }


        /// <summary>
        /// سوال بر مبنای کارخواست
        /// </summary>
        public long? RequestId { get; private set; }

        public string RequestTitle { get; set; }


        /// <summary>
        /// سوال بر مبنای موضوعات
        /// </summary>
        public long? SubjectId { get; private set; }

        public string SubjectTitle { get; set; }

    }

}