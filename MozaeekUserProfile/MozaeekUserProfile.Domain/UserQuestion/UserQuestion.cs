using System;
using System.Collections.Generic;
using System.Linq;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Enum;
using MozaeekUserProfile.Common.ExtensionMethod;
using Newtonsoft.Json;

namespace MozaeekUserProfile.Domain
{
    public class UserQuestion
    {
        protected UserQuestion()
        {

        }
        public long Id { get; set; }

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

        /// <summary>
        /// متن سوال
        /// </summary>
        public string TextDescription { get; private set; }

        /// <summary>
        /// عنوان سوال
        /// </summary>
        public string QuestionTitle { get; private set; }

        public QuestionAnswerType QuestionType { get; private set; }
        public QuestionAnswerType AnswerType { get; private set; }
        public long UserId { get; private set; }
        public virtual User User { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public UserQuestionState LastQuestionState { get; private set; }
        public virtual ICollection<QuestionState> QuestionStates { get; private set; }
        public UserWalletDebit UserWalletDebit { get; private set; }
        public string QuestionCodePreFix { get; private set; }
        public string QuestionCodeNo { get; private set; }
        public Guid QuestionUniqId { get; private set; }
        public ICollection<UserQuestionAttachment> UserQuestionAttachments { get; private set; }
        public string VoiceHttpPath { get; set; }
        public long? VoiceFileId { get; set; }
        public TechnicianType TechnicianType { get; private set; }
        public int UnitPrice { get; private set; }
        public int TechnicianPriceShare { get; private set; }
        public int SystemPriceShare { get; private set; }
        public PriceCurrencyType PriceCurrencyType { get; private set; }

        public string QuestionCode()
        {
            return $"{QuestionCodePreFix}{QuestionCodeNo}";
        }

        #region QuestionRequestCreation
        public UserQuestion(string questionTitle, long userId, string description, UserQuestionRequestDetail requestDetail, bool isTextAnswer, ProperPriceResult properPrice
          , string questionCodePreFix, UserWallet userWallet, TechnicianType technicianType, List<UserQuestionAttachment> attachments)
        {
            //Check User Credit For 
            if (userWallet == null || userWallet.AvailableAmount < properPrice.UnitPrice)
            {
                throw new System.Exception("خطای اعتبار کاربر");
            }
            QuestionTitle = questionTitle.Recheck();
            this.UserId = userId;
            TextDescription = description.Recheck();
            CreateDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            QuestionType = QuestionAnswerType.Text;
            RequestId = requestDetail.RequestId;
            RequestTitle = requestDetail.RequestTitle;
            AnswerType = isTextAnswer ? QuestionAnswerType.Text : QuestionAnswerType.Voice;
            var lastQuestionState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                UserQuestionState = UserQuestionState.SendByUser,
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, RequestId, AnswerType, TextDescription }),
            };
            QuestionStates = new List<QuestionState>() { lastQuestionState };
            LastQuestionState = lastQuestionState.UserQuestionState;
            QuestionUniqId = Guid.NewGuid();
            QuestionCodePreFix = questionCodePreFix;
            QuestionUniqId = Guid.NewGuid();
            UserQuestionAttachments = attachments;
            TechnicianType = technicianType;
            UnitPrice = properPrice.UnitPrice;
            TechnicianPriceShare = properPrice.TechnicianShare;
            SystemPriceShare = properPrice.SystemShare;
            PriceCurrencyType = (PriceCurrencyType)properPrice.PriceCurrencyId;

        }

        public UserQuestion(string questionTitle, UserQuestionRequestDetail requestDetail, QuestionAnswerType answerType, ProperPriceResult properPrice,
            long userId, string questionCodePreFix, string voiceHttpPath, long? voiceFileId, UserWallet userWallet, TechnicianType technicianType, List<UserQuestionAttachment> attachments)
        {
            //Check User Credit For 
            if (userWallet == null || userWallet.AvailableAmount < properPrice.UnitPrice)
            {
                throw new System.Exception("خطای اعتبار کاربر");
            }

            QuestionTitle = questionTitle.Recheck();
            RequestId = requestDetail.RequestId;
            RequestTitle = requestDetail.RequestTitle;
            QuestionType = QuestionAnswerType.Voice;
            AnswerType = answerType;
            ModifiedDate = DateTime.Now;
            CreateDate = DateTime.Now;
            UserId = userId;
            QuestionCodePreFix = questionCodePreFix;
            var lastQuestionState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                UserQuestionState = UserQuestionState.SendByUser,
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, RequestId, AnswerType, TextDescription }),
            };
            QuestionStates = new List<QuestionState>() { lastQuestionState };
            LastQuestionState = lastQuestionState.UserQuestionState;
            QuestionUniqId = Guid.NewGuid();
            QuestionUniqId = Guid.NewGuid();
            VoiceHttpPath = voiceHttpPath;
            VoiceFileId = voiceFileId;
            UserQuestionAttachments = attachments;
            TechnicianType = technicianType;
            UnitPrice = properPrice.UnitPrice;
            TechnicianPriceShare = properPrice.TechnicianShare;
            SystemPriceShare = properPrice.SystemShare;
            PriceCurrencyType = (PriceCurrencyType)properPrice.PriceCurrencyId;
        }

        #endregion

        #region QuestionSubjectCreation

        public UserQuestion(string questionTitle, long userId, string description, UserQuestionSubjectDetail subjectDetail, bool isTextAnswer, ProperPriceResult properPrice
          , string questionCodePreFix, UserWallet userWallet, TechnicianType technicianType, List<UserQuestionAttachment> attachments)
        {
            //Check User Credit For 
            if (userWallet == null || userWallet.AvailableAmount < properPrice.UnitPrice)
            {
                throw new System.Exception("خطای اعتبار کاربر");
            }
            QuestionTitle = questionTitle.Recheck();
            this.UserId = userId;
            TextDescription = description.Recheck();
            CreateDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
            QuestionType = QuestionAnswerType.Text;
            SubjectId = subjectDetail.SubjectId;
            SubjectTitle = subjectDetail.SubjectTitle;
            AnswerType = isTextAnswer ? QuestionAnswerType.Text : QuestionAnswerType.Voice;
            var lastQuestionState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                UserQuestionState = UserQuestionState.SendByUser,
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, RequestId, AnswerType, TextDescription }),
            };
            QuestionStates = new List<QuestionState>() { lastQuestionState };
            LastQuestionState = lastQuestionState.UserQuestionState;
            QuestionUniqId = Guid.NewGuid();
            QuestionCodePreFix = questionCodePreFix;
            QuestionUniqId = Guid.NewGuid();
            UserQuestionAttachments = attachments;
            TechnicianType = technicianType;
            UnitPrice = properPrice.UnitPrice;
            TechnicianPriceShare = properPrice.TechnicianShare;
            SystemPriceShare = properPrice.SystemShare;
            PriceCurrencyType = (PriceCurrencyType)properPrice.PriceCurrencyId;
        }

        public UserQuestion(string questionTitle, UserQuestionSubjectDetail subjectDetail, QuestionAnswerType answerType, ProperPriceResult properPrice,
            long userId, string questionCodePreFix, string voiceHttpPath, long? voiceFileId, UserWallet userWallet, TechnicianType technicianType, List<UserQuestionAttachment> attachments)
        {
            //Check User Credit For 
            if (userWallet == null || userWallet.AvailableAmount < properPrice.UnitPrice)
            {
                throw new System.Exception("خطای اعتبار کاربر");
            }

            QuestionTitle = questionTitle.Recheck();
            SubjectId = subjectDetail.SubjectId;
            SubjectTitle = subjectDetail.SubjectTitle;
            QuestionType = QuestionAnswerType.Voice;
            AnswerType = answerType;
            ModifiedDate = DateTime.Now;
            CreateDate = DateTime.Now;
            UserId = userId;
            QuestionCodePreFix = questionCodePreFix;
            var lastQuestionState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                UserQuestionState = UserQuestionState.SendByUser,
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, RequestId, AnswerType, TextDescription }),
            };
            QuestionStates = new List<QuestionState>() { lastQuestionState };
            LastQuestionState = lastQuestionState.UserQuestionState;
            QuestionUniqId = Guid.NewGuid();
            VoiceHttpPath = voiceHttpPath;
            VoiceFileId = voiceFileId;
            UserQuestionAttachments = attachments;
            TechnicianType = technicianType;
            UnitPrice = properPrice.UnitPrice;
            TechnicianPriceShare = properPrice.TechnicianShare;
            SystemPriceShare = properPrice.SystemShare;
            PriceCurrencyType = (PriceCurrencyType)properPrice.PriceCurrencyId;
        }

        #endregion

        public void Cancel(UserWallet userWallet)
        {
            if (LastQuestionState == UserQuestionState.CanceledByUser)
            {
                throw new System.Exception("امکان کنسل کردن سوال وجود ندارد.");
            }
            var userDebit = userWallet.UserWalletDebits.SingleOrDefault(m => m.UserQuestionId == Id);
            var newAmount = userDebit.Amount * -1;
            userWallet.DecreaseUserCredit(Id, newAmount, $"Cancel {this.QuestionCode()}");
            var lastQuestionState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                Description = "Question Cancel By User",
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, QuestionTitle, SubjectId, AnswerType, VoiceHttpPath, TextDescription }),
                UserQuestionState = UserQuestionState.CanceledByUser
            };
            QuestionStates.Add(lastQuestionState);
            LastQuestionState = lastQuestionState.UserQuestionState;
        }

        public void ChangeState(UserQuestionState inputState)
        {
            var newState = new QuestionState()
            {
                CreateDate = DateTime.Now,
                SerializedEvent = JsonConvert.SerializeObject(new
                { UserId, QuestionType, QuestionTitle, SubjectId, AnswerType, VoiceHttpPath, TextDescription }),
                UserQuestionState = inputState
            };
            QuestionStates.Add(newState);
            LastQuestionState = newState.UserQuestionState;
        }
    }
}