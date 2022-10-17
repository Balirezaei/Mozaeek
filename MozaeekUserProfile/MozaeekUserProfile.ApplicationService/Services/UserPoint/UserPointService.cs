using System.Threading.Tasks;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Mapper;

namespace MozaeekUserProfile.ApplicationService.Services.UserPoint
{
    public interface IUserPointService
    {
        Task<UserPointInputDto> CreateUserPoint(UserPointInputDto dto);
        Task<UserPointInputDto> GetCurrentUserPoint(long userId);
    }
    public class UserPointService : IUserPointService
    {
        private readonly IUserPointRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserPointService(IUserPointRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserPointInputDto> CreateUserPoint(UserPointInputDto dto)
        {
            var point = new Domain.UserPoint(0, dto.PointId, dto.UserId, dto.PointTitle);
            await _repository.CreateUserPoint(point);
            await _unitOfWork.CommitAsync();
            return dto;

        }

        public async Task<UserPointInputDto> GetCurrentUserPoint(long userId)
        {
            var userPoint = await _repository.GetActivePoint(userId);
            if (userPoint == null)
            {
                return null;
            }
            return userPoint.GetUserPointInputDto();
        }
    }
}