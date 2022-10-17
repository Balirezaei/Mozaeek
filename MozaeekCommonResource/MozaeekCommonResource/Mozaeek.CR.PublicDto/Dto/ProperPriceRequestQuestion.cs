namespace Mozaeek.CR.PublicDto
{
    public class ProperPriceRequestQuestion
    {
        public string RequestTitle { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public ProperPriceResult ProperPriceResult { get; set; }

        public ProperPriceRequestQuestion(string requestTitle, TechnicianType technicianType, ProperPriceResult properPriceResult)
        {
            RequestTitle = requestTitle;
            TechnicianType = technicianType;
            ProperPriceResult = properPriceResult;
        }
    }
}