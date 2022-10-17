using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mozaeek.CR.PublicDto;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Core.Core.MessagePublisher;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;

namespace MozaeekUserProfile.ApplicationService.Services
{
    public interface IQuestionWorkflowService
    {
        Task<List<UserQuestionForTechnicianProcess>> GetAllQuestionForTechnicianProcesses();

        Task<UpdateUserQuestionStateResult> UpdateQuestionState(UpdateUserQuestionStateInput input);
    }

    public class QuestionWorkflowService : IQuestionWorkflowService
    {
        private readonly IUserQuestionRepository _userQuestionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public QuestionWorkflowService(IUserQuestionRepository userQuestionRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher, IUserRepository userRepository)
        {
            _userQuestionRepository = userQuestionRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _userRepository = userRepository;
        }

        public async Task<List<UserQuestionForTechnicianProcess>> GetAllQuestionForTechnicianProcesses()
        {
            var list = await _userQuestionRepository
                .GetAll(m => m.LastQuestionState == UserQuestionState.SendByUser)
                .Include(m => m.User)
                .ToListAsync();
            return list.Select(m => new UserQuestionForTechnicianProcess
            {
                UserId = m.UserId,
                AnswerType = m.AnswerType,
                QuestionType = m.QuestionType,
                RequestTitle = m.RequestTitle,
                SubjectTitle = m.SubjectTitle,
                QuestionCode = m.QuestionCode(),
                TechnicianType = m.TechnicianType,
                UserQuestionState = m.LastQuestionState,
                QuestionTitle = m.QuestionTitle,
                QuestionId = m.Id,
                VoiceHttpPath = m.VoiceHttpPath,
                VoiceFileId = m.VoiceFileId,
                UnitPrice = m.UnitPrice,
                SystemPriceShare = m.SystemPriceShare,
                PriceCurrencyType = m.PriceCurrencyType,
                TechnicianPriceShare = m.TechnicianPriceShare,
                Id = m.Id,
                QuestionTextDescription = m.TextDescription,
                UserDeviceId = m.User.DeviceId,
                UserFullName = m.User.FullName(),
                CreateDate = m.CreateDate
            }).ToList();
        }

        public async Task<UpdateUserQuestionStateResult> UpdateQuestionState(UpdateUserQuestionStateInput input)
        {
            var question = await _userQuestionRepository.FindWithStates(input.QuestionId);
            var user = await _userRepository.GetUserById(question.UserId);
            question.ChangeState(input.State);
            await _unitOfWork.CommitAsync();
            var @event = new UserQuestionEventBuilder(question, user).BuildNotificationEventByState(input.State);
            if (@event != null)
            {
                await _publisher.PublishAsync(@event);
            }



            return new UpdateUserQuestionStateResult()
            {
                QuestionId = input.QuestionId,
                State = input.State
            };
        }
    }
}