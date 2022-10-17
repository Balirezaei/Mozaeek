namespace Mozaeek.CR.PublicDto
{
    public class ProperPriceSubjectQuestion
    {
        public string SubjectTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public ProperPriceResult ProperPriceResult { get; set; }

        public ProperPriceSubjectQuestion(string subjectTitle, TechnicianType technicianType, ProperPriceResult properPriceResult)
        {
            SubjectTitle = subjectTitle;
            TechnicianType = technicianType;
            ProperPriceResult = properPriceResult;
        }
    }
}