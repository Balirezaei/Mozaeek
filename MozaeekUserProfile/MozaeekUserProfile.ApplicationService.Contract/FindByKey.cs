using MozaeekUserProfile.Core.Core.Base;

namespace MozaeekUserProfile.ApplicationService.Contract
{
    public class FindByKey : Query
    {
        public FindByKey(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}