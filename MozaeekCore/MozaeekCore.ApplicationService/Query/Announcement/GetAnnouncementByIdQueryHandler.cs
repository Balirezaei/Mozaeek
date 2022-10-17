using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAnnouncementByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, AnnouncementDto>
    {
        private readonly IAnnouncementQueryService _repository;
        public GetAnnouncementByIdQueryHandler(IAnnouncementQueryService repository)
        {
            _repository = repository;
        }

        public async Task<AnnouncementDto> HandleAsync(FindByKey query)
        {
            var announcement = await _repository.Get(query.Id);
            return announcement.GetAnnouncement();
        }
    }
}