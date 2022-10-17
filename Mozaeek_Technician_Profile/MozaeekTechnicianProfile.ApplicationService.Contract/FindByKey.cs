using MozaeekTechnicianProfile.Core.Core.Base;

namespace MozaeekTechnicianProfile.ApplicationService.Contract
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