namespace Api_Aggregator.Contract.MediationDtos
{
    //ToDo : Move to Common Resource
    public class SubjectWithPriceDetailDto
    {
        public long SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int PriceAmount { get; set; }
        public int TechnicianCount { get; set; }
    }
}