using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karmizban.Support.ApplicationService.Contract;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;

namespace Karmizban.Support.ApplicationService
{
    public interface IUserSuggestedSupportService
    {
        Task<CreateUserSuggestedSupportResult> Create(CreateUserSuggestedSupportCommand cmd);
        Task<List<UserSuggestedSupportDto>> GetAll();
    }
    public class UserSuggestedSupportService : IUserSuggestedSupportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSuggestedSupportRepository _userSuggestedSupportRepository;

        public UserSuggestedSupportService(IUserSuggestedSupportRepository userSuggestedSupportRepository, IUnitOfWork unitOfWork)
        {
            _userSuggestedSupportRepository = userSuggestedSupportRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserSuggestedSupportResult> Create(CreateUserSuggestedSupportCommand cmd)
        {
            var domain = new UserSuggestedSupport(cmd.Title, cmd.Description);
            await _userSuggestedSupportRepository.CreateUserSuggest(domain);
            await _unitOfWork.CommitAsync();
            return new CreateUserSuggestedSupportResult()
            {
                Title = domain.Title,
                Id = domain.Id
            };
        }

        public async Task<List<UserSuggestedSupportDto>> GetAll()
        {
            var list = await _userSuggestedSupportRepository.GetAll();
            return list.Select(m => new UserSuggestedSupportDto
            {
                Title = m.Title,
                Id = m.Id
            }).ToList();
        }
    }
}