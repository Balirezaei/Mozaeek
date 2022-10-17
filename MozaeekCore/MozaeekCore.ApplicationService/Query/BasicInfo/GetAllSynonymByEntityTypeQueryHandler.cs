using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.BasicInfo
{
    public class GetAllSynonymByEntityTypeQueryHandler : IBaseAsyncQueryHandler<FindSynonymByEntityType, List<SynonymsDto>>
    {
        private readonly ISynonymQueryService _synonymQueryService;

        public GetAllSynonymByEntityTypeQueryHandler(ISynonymQueryService synonymQueryService)
        {
            _synonymQueryService = synonymQueryService;
        }

        public async Task<List<SynonymsDto>> HandleAsync(FindSynonymByEntityType query)
        {
            var queries = await _synonymQueryService.GetByPredicate(m => m.EntityType == query.EntityType);
            return queries.Select(m => new SynonymsDto()
            {
                Synonym = m.Synonym,
                Id = m.Id,
                Title = m.Title,
                EntityType = m.EntityType,
                EntityDescription = m.EntityType.ToString()
            }).ToList();
        }
    }
}