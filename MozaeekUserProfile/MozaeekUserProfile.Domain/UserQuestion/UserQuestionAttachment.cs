namespace MozaeekUserProfile.Domain
{
    public class UserQuestionAttachment
    {
        public long Id { get; private set; }
        public virtual UserQuestion UserQuestion { get; private set; }
        public long UserQuestionId { get; private set; }
        public string FileHttpAddress { get; private set; }
        public long FileId { get; private set; }

        protected UserQuestionAttachment()
        {

        }

        public UserQuestionAttachment(string fileHttpAddress, long fileId)
        {
            FileHttpAddress = fileHttpAddress;
            FileId = fileId;
        }
    }
}