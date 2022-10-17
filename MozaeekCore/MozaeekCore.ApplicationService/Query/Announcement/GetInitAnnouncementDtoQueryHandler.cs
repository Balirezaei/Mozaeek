using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetInitAnnouncementDtoQueryHandler : IBaseAsyncQueryHandler<Nothing, InitAnnouncementDto>
    {
        private readonly IPointQueryService _pointQueryService; 
        private readonly ILabelQueryService _labelQueryService;
        private readonly ISubjectQueryService _subjectQueryService;
        private readonly IRequestOrgQueryService _requestOrgQueryService;


        public GetInitAnnouncementDtoQueryHandler(IPointQueryService pointQueryService, ILabelQueryService labelQueryService, ISubjectQueryService subjectQueryService, IRequestOrgQueryService requestOrgQueryService)
        {
            _pointQueryService = pointQueryService;
            _labelQueryService = labelQueryService;
            _subjectQueryService = subjectQueryService;
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<InitAnnouncementDto> HandleAsync(Nothing query)
        {
            var points = (await _pointQueryService.GetByPredicate(_ => true))
                .Select(m => new PointDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ParentId = m.ParentId
                }).ToList();

            var labels = await _labelQueryService.GetByPredicate(_ => true);
            var subjects = await _subjectQueryService.GetByPredicate(_ => true);
            var requestOrgs = await _requestOrgQueryService.GetByPredicate(_ => true);

            var requestOrgsTotal = requestOrgs.Select(z => new RequestOrgDto()
            {
                Id = z.Id,
                Title = z.Title,
                ParentId = z.ParentId
            });

            var subjectTotal = subjects.Select(z => new SubjectDto()
            {
                Id = z.Id,
                Title = z.Title,
                ParentId = z.ParentId
            });


            var labelTotal = labels.Select(z => new LabelDto()
            {
                Id = z.Id,
                Title = z.Title,
                ParentId = z.ParentId
            });


            return new InitAnnouncementDto()
            {
                Points = points,
                RequestOrgs = requestOrgsTotal.OrderBy(m => m.Id).ToList(),
                Labels = labelTotal.OrderBy(m => m.Id).ToList(),
                Subjects = subjectTotal.OrderBy(m => m.Id).ToList(),
            };
        }
    }
}