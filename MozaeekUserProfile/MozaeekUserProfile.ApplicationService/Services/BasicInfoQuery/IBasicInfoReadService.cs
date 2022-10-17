using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Persistence.Mongo;

namespace MozaeekUserProfile.ApplicationService.Services.BasicInfoQuery
{
    public interface IBasicInfoReadService
    {
        Task<List<LabelQueryDto>> GetAllParentLabel();
        Task<List<LabelQueryDto>> GetAllChildrenLabel(long parentId);
        Task<List<SubjectQueryDto>> GetAllParentSubject();
        Task<List<SubjectQueryDto>> GetAllChildrenSubject(long parentId);
        Task<List<RequestOrgQueryDto>> GetAllParentRequestOrg();
        Task<List<RequestOrgQueryDto>> GetAllChildrenRequestOrg(long parentId);
        // Task<List<RequestTargetQueryDto>> GetAllRequestTarget();
        Task<List<PointQueryDto>> GetAllCity();
    }

    public class BasicInfoReadService : IBasicInfoReadService
    {
        private readonly IMongoRepository _mongoRepository;

        public BasicInfoReadService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<LabelQueryDto>> GetAllParentLabel()
        {
            var list = await _mongoRepository.LabelQueryCollection
                .Find(m => m.ParentId == null)
                .ToListAsync();

            return list.Select(m => new LabelQueryDto
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }

        public async Task<List<LabelQueryDto>> GetAllChildrenLabel(long parentId)
        {
            var list = await _mongoRepository.LabelQueryCollection
                .Find(m => m.ParentId == parentId)
                .ToListAsync();

            return list.Select(m => new LabelQueryDto
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }

        public async Task<List<SubjectQueryDto>> GetAllParentSubject()
        {
            var list = await _mongoRepository.SubjectQueryCollection
                .Find(m => m.ParentId == null)
                .ToListAsync();

            return list.Select(m => new SubjectQueryDto
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }

        public async Task<List<SubjectQueryDto>> GetAllChildrenSubject(long parentId)
        {
            var list = await _mongoRepository.SubjectQueryCollection
                .Find(m => m.ParentId == parentId)
                .ToListAsync();

            return list.Select(m => new SubjectQueryDto
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }

        public async Task<List<RequestOrgQueryDto>> GetAllParentRequestOrg()
        {
            var list = await _mongoRepository.RequestOrgQueryCollection
                .Find(m => m.ParentId == null)
                .ToListAsync();

            return list.Select(m => new RequestOrgQueryDto()
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }
        public async Task<List<RequestOrgQueryDto>> GetAllChildrenRequestOrg(long parentId)
        {
            var list = await _mongoRepository.RequestOrgQueryCollection
                .Find(m => m.ParentId == parentId)
                .ToListAsync();

            return list.Select(m => new RequestOrgQueryDto()
            {
                Title = m.Title,
                Id = m.Id,
                HasChild = m.HasChild
            }).ToList();
        }

        // public async Task<List<RequestTargetQueryDto>> GetAllRequestTarget()
        // {
        //     var list = await _mongoRepository.RequestTargetQueryCollection
        //         .Find(m => m.Id > 0)
        //         .ToListAsync();
        //
        //     return list.Select(m => new RequestTargetQueryDto()
        //     {
        //         Title = m.Title,
        //         Id = m.Id
        //     }).ToList();
        // }

        public async Task<List<PointQueryDto>> GetAllCity()
        {
            var list = await _mongoRepository.PointQueryCollection
                .Find(m => m.HasChild == false)
                .ToListAsync();

            return list.Select(m => new PointQueryDto()
            {
                Title = m.Title,
                Id = m.Id
            }).ToList();
        }
    }
}