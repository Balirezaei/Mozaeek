namespace MozaeekCore.ApplicationService.Contract
{
    public class ProperPriceForSubjectQuestionQuery
    {
        public ProperPriceForSubjectQuestionQuery(long subjectId)
        {
            SubjectId = subjectId;
        }

        public long SubjectId { get; set; }
    }
}