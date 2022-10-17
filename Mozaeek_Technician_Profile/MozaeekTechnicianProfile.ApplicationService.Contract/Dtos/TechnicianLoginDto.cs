namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class TechnicianLoginDto
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        //public bool HasCompleted { get; set; } = false;
        public long TechnicianId { get; set; }
    }
}
