using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Enum;

namespace MozaeekCore.Persistense.MongoDb
{
    public class SynonymsQuery
    {
        public SynonymsQuery(long id, string title, string synonym, EntityType entityType)
        {
            Id = id;
            Title = title.Recheck();
            Synonym = synonym.Recheck();
            EntityType = entityType;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Synonym { get; set; }
        public EntityType EntityType { get; set; }
    }

   
}