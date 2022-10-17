namespace MozaeekUserProfile.Domain
{
    public class UserQuestionSubjectDetail
    {
        public UserQuestionSubjectDetail(string subjectTitle, long subjectId)
        {
            SubjectTitle = subjectTitle;
            SubjectId = subjectId;
        }

        public string SubjectTitle { get; set; }
        public long SubjectId { get; set; }
    }
}