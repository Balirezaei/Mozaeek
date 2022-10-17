using System;
using Karmizban.Support.Common;

namespace Karmizban.Support.Domain
{
    public class UserRequestSupportAnswer
    {
        public long Id { get; private set; }
        public long UserRequestSupportId { get;private set; }
        public virtual UserRequestSupport UserRequestSupport { get;private set; }
        public string Title { get;private set; }
        public string AnswerDescription { get;private set; }
        public DateTime CreateDate { get; set; }

        public UserRequestSupportAnswer(string title, string answerDescription)
        {
            Title = title.Recheck();
            AnswerDescription = answerDescription.Recheck();
            CreateDate=DateTime.Now;
        }
    }
}