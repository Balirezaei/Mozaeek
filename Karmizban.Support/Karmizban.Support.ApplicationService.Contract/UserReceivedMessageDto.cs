namespace Karmizban.Support.ApplicationService.Contract
{
    public class UserReceivedMessageDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CreateDate { get; set; }
    }
    public class GetUserReceivedMessageContract
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long UserId { get; set; }
    }
}