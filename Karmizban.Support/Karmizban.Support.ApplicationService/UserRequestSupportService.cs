using System.Threading.Tasks;
using Karmizban.Support.ApplicationService.Contract;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;

namespace Karmizban.Support.ApplicationService
{
    public interface IUserRequestSupportService
    {
        Task<CreateUserRequestSupportResult> Create(CreateUserRequestSupportCommand cmd);
        Task<UserRequestSupportAnswerResult> Answer(UserRequestSupportAnswerCommand cmd);
    }

    public class UserRequestSupportService : IUserRequestSupportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRequestSupportRepository _userRequestSupportRepository;
        private readonly IUserSuggestedSupportRepository _userSuggestedSupportRepository;
        private readonly IUserRequestSupportAnswerRepository _userRequestSupportAnswerRepository;

        public UserRequestSupportService(IUserRequestSupportRepository userRequestSupportRepository, IUserSuggestedSupportRepository userSuggestedSupportRepository, IUnitOfWork unitOfWork, IUserRequestSupportAnswerRepository userRequestSupportAnswerRepository)
        {
            _userRequestSupportRepository = userRequestSupportRepository;
            _userSuggestedSupportRepository = userSuggestedSupportRepository;
            _unitOfWork = unitOfWork;
            _userRequestSupportAnswerRepository = userRequestSupportAnswerRepository;
        }

        public async Task<CreateUserRequestSupportResult> Create(CreateUserRequestSupportCommand cmd)
        {
            string title = null;

            if (cmd.UserSuggestedSupportId != null)
            {
                var suggested = await _userSuggestedSupportRepository.Find(cmd.UserSuggestedSupportId.Value);
                title = suggested?.Title;
            }

            var domain = new UserRequestSupport(
                new User { UserId = cmd.UserId, UserFullName = cmd.UserFullName },
                cmd.UserSuggestedSupportId,
                cmd.QuestionId,
                cmd.QuestionCode,
                title,
                cmd.Description);

            await _userRequestSupportRepository.Create(domain);
            await _unitOfWork.CommitAsync();
            return new CreateUserRequestSupportResult()
            {
                Id = domain.Id
            };
        }

        public async Task<UserRequestSupportAnswerResult> Answer(UserRequestSupportAnswerCommand cmd)
        {
            var request = await _userRequestSupportRepository.Find(cmd.UserRequestSupportId);
            request.Answer(new UserRequestSupportAnswer(cmd.Title,cmd.Description));
            await _unitOfWork.CommitAsync();
            return new UserRequestSupportAnswerResult();
        }
    }
}