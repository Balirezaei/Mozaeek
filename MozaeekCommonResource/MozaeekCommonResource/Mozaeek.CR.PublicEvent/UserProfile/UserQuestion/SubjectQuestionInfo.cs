namespace Mozaeek.CR.PublicEvent.UserProfile
{
    public class SubjectQuestionInfo
    {
        public long SubjectId { get; set; }
        public string SubjectTitle { get; set; }

        public SubjectQuestionInfo(long subjectId, string subjectTitle)
        {
            SubjectId = subjectId;
            SubjectTitle = subjectTitle;
        }
    }
}