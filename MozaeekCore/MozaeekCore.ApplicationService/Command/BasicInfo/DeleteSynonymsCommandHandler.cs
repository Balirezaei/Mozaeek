using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteSynonymsCommandHandler : IBaseAsyncCommandHandler<DeleteSynonymsCommand, Nothing>
    {
        private readonly ISynonymQueryService _synonymQueryService;

        public DeleteSynonymsCommandHandler(ISynonymQueryService synonymQueryService)
        {
            _synonymQueryService = synonymQueryService;
        }

        public async Task<Nothing> HandleAsync(DeleteSynonymsCommand cmd)
        {
            await _synonymQueryService.Remove(cmd.Id);
            return new Nothing();
        }
    }
}