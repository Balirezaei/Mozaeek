using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateSynonymsCommand : Command
    {
        public string Title { get; set; }
        public string Synonym { get; set; }
        public EntityType EntityType { get; set; }
    }
    public class DeleteSynonymsCommand : Command
    {
        public DeleteSynonymsCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
    public class SynonymsDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Synonym { get; set; }
        public EntityType EntityType { get; set; }
        public string EntityDescription { get; set; }
    }
    public class CreateSynonymsCommandResult { }

    public class FindSynonymByEntityType
    {
        public FindSynonymByEntityType(EntityType entityType)
        {
            EntityType = entityType;
        }

        public EntityType EntityType { get; private set; }
    }
}