namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserProfileOutputDto
    {        
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Id { get; set; }
    }
}
