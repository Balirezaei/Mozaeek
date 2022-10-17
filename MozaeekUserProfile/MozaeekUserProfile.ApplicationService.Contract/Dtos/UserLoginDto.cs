namespace MozaeekUserProfile.ApplicationService.Contract.Dtos
{
    public class UserLoginDto
    {
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public bool HasCompleted { get; set; } = false;
        public long UserId { get; set; }
    }
}
