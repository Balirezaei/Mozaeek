using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateSynonymsCommandHandler : IBaseAsyncCommandHandler<CreateSynonymsCommand, CreateSynonymsCommandResult>
    {
        private readonly ISynonymQueryService _synonymQueryService;

        public CreateSynonymsCommandHandler(ISynonymQueryService synonymQueryService)
        {
            _synonymQueryService = synonymQueryService;
        }

        public async Task<CreateSynonymsCommandResult> HandleAsync(CreateSynonymsCommand cmd)
        {
            var id = await _synonymQueryService.GetNextId();
            await _synonymQueryService.Create(new SynonymsQuery(id, cmd.Title, cmd.Synonym, cmd.EntityType));
            return new CreateSynonymsCommandResult();
        }
    }
}