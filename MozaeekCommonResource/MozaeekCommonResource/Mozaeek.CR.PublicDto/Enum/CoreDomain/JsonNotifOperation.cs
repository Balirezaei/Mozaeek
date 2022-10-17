namespace Mozaeek.CR.PublicDto.Enum
{
    public class JsonNotifOperation
    {
        public JsonNotifOperationType Type { get; set; }
        public object Detail { get; set; }
    }

    public enum JsonNotifOperationType
    {
        UserQuestionResponse = 1,
    }

    public class UserQuestionResponse
    {
        public bool TechnicianIsFound { get; set; }
        public string QuestionId { get; set; }
        public string Response { get; set; }
        public int TimeToAnswer { get; set; }
        public string TimeUnitDescription { get; set; }
    }
}