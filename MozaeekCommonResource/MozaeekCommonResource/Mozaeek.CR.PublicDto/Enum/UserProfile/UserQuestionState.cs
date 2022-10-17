namespace Mozaeek.CR.PublicDto
{
    public enum UserQuestionState
    {
        SendByUser = 1,
        SearchForTechnician = 2,
        SendToTechnician = 3,
        AnsweredByTechnician = 4,
        CanceledByUser = 5,
        TechnicianNotFound = 6
    }
}