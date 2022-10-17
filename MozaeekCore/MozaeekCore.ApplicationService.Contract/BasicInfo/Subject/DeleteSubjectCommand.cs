using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class DeleteSubjectCommand : Command
    {
        public DeleteSubjectCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}