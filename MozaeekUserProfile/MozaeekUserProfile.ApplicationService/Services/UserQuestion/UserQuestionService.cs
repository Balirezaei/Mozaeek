using System;
using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using Mozaeek.CR.PublicDto.Dto;
using Mozaeek.CR.PublicEvent.UserProfile;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Core.Core;
using MozaeekUserProfile.Core.Core.MessagePublisher;
using MozaeekUserProfile.Domain;
using MozaeekUserProfile.Domain.Contracts;

namespace MozaeekUserProfile.ApplicationService.Services
{
    public interface IUserQuestionService
    {
        Task<CreateQuestionResult> CreateTextRequestQuestion(CreateTextRequestQuestionDto dto);
        Task<CreateQuestionResult> CreateVoiceRequestQuestion(CreateVoiceRequestQuestionDto dto);

        Task<CreateQuestionResult> CreateTextSubjectQuestion(CreateTextSubjectQuestionDto dto);
        Task<CreateQuestionResult> CreateVoiceSubjectQuestion(CreateVoiceSubjectQuestionDto dto);
        Task<CancelQuestionResult> CancelQuestion(CancelQuestionInput input);
    }

    public class UserQuestionService : IUserQuestionService
    {
        private readonly IUserQuestionRepository _userQuestionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserWalletRepository _userWalletRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UserQuestionService(IUserQuestionRepository userQuestionRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher, IUserRepository userRepository, IUserWalletRepository userWalletRepository)
        {
            _userQuestionRepository = userQuestionRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
        }

        public async Task<CreateQuestionResult> CreateTextRequestQuestion(CreateTextRequestQuestionDto dto)
        {
            try
            {
                var questionCodePrefix = dto.TechnicianType == TechnicianType.Agent ? "AGN" : (dto.TechnicianType == TechnicianType.Expert ? "EXP" : "GUD");

                var userWallet = await _userWalletRepository.GetWithDebit(dto.UserId, dto.ProperPrice.PriceCurrencyId);
                var attachment =
                    dto.QuestionAttachments?
                        .Select(m => new UserQuestionAttachment(m.FileHttpAddress, m.FileId))
                        .ToList();

                var userQuestion = new UserQuestion(dto.QuestionTitle, dto.UserId, dto.Description, new UserQuestionRequestDetail(dto.RequestTitle, dto.RequestId), dto.IsTextAnswer, dto.ProperPrice, questionCodePrefix, userWallet, dto.TechnicianType, attachment);

                await _unitOfWork.BeginTransaction();
                await _userQuestionRepository.CreateUserQuestion(userQuestion);
                await _unitOfWork.CommitAsync();

                userWallet.DecreaseUserCredit(userQuestion.Id, dto.ProperPrice.UnitPrice, $"{userQuestion.QuestionCode()}");

                await _unitOfWork.CommitAsync();

                var user = await _userRepository.GetUserById(dto.UserId);

                //await _publisher.PublishAsync(new UserQuestionEventBuilder(userQuestion, dto.QuestionAttachments, user, dto.ProperPrice).BuildTextRequest());

                await _unitOfWork.CommitTransaction();

                return new CreateQuestionResult()
                {
                    QuestionCode = userQuestion.QuestionCode(),
                    QuestionId = userQuestion.Id
                };
            }
            catch (System.Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<CreateQuestionResult> CreateVoiceRequestQuestion(CreateVoiceRequestQuestionDto dto)
        {
            try
            {
                var questionCodePrefix = dto.TechnicianType == TechnicianType.Agent ? "AGN" : (dto.TechnicianType == TechnicianType.Expert ? "EXP" : "GUD");

                var userWallet = await _userWalletRepository.GetWithDebit(dto.UserId, dto.ProperPrice.PriceCurrencyId);

                var attachment =
                    dto.QuestionAttachments?
                        .Select(m => new UserQuestionAttachment(m.FileHttpAddress, m.FileId))
                        .ToList();

                var userQuestion = new UserQuestion(dto.QuestionTitle, new UserQuestionRequestDetail(dto.RequestTitle, dto.RequestId), (dto.IsTextAnswer ? QuestionAnswerType.Text : QuestionAnswerType.Voice), dto.ProperPrice, dto.UserId
                    , questionCodePrefix, dto.Voice.FileHttpAddress, dto.Voice.FileId, userWallet, dto.TechnicianType, attachment);

                await _unitOfWork.BeginTransaction();
                await _userQuestionRepository.CreateUserQuestion(userQuestion);
                await _unitOfWork.CommitAsync();

                userWallet.DecreaseUserCredit(userQuestion.Id, dto.ProperPrice.UnitPrice, $"{userQuestion.QuestionCode()}");

                await _unitOfWork.CommitAsync();

                var user = await _userRepository.GetUserById(dto.UserId);

                //await _publisher.PublishAsync(new UserQuestionEventBuilder(userQuestion, dto.QuestionAttachments, user, dto.ProperPrice).BuildVoiceRequest());

                await _unitOfWork.CommitTransaction();

                return new CreateQuestionResult()
                {
                    QuestionCode = userQuestion.QuestionCode(),
                    QuestionId = userQuestion.Id
                };
            }
            catch (System.Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }


        public async Task<CreateQuestionResult> CreateTextSubjectQuestion(CreateTextSubjectQuestionDto dto)
        {
            try
            {
                var questionCodePrefix = dto.TechnicianType == TechnicianType.Agent ? "AGN" : (dto.TechnicianType == TechnicianType.Expert ? "EXP" : "GUD");

                var userWallet = await _userWalletRepository.GetWithDebit(dto.UserId, dto.ProperPrice.PriceCurrencyId);
                var attachment =
                    dto.QuestionAttachments?
                        .Select(m => new UserQuestionAttachment(m.FileHttpAddress, m.FileId))
                        .ToList();

                var userQuestion = new UserQuestion(dto.QuestionTitle, dto.UserId, dto.Description, new UserQuestionSubjectDetail(dto.SubjectTitle, dto.SubjectId), dto.IsTextAnswer, dto.ProperPrice, questionCodePrefix, userWallet, dto.TechnicianType, attachment);

                await _unitOfWork.BeginTransaction();
                await _userQuestionRepository.CreateUserQuestion(userQuestion);
                await _unitOfWork.CommitAsync();

                userWallet.DecreaseUserCredit(userQuestion.Id, dto.ProperPrice.UnitPrice, $"{userQuestion.QuestionCode()}");

                await _unitOfWork.CommitAsync();

                var user = await _userRepository.GetUserById(dto.UserId);

                //await _publisher.PublishAsync(new UserQuestionEventBuilder(userQuestion, dto.QuestionAttachments, user, dto.ProperPrice).BuildTextSubject());

                await _unitOfWork.CommitTransaction();

                return new CreateQuestionResult()
                {
                    QuestionCode = userQuestion.QuestionCode(),
                    QuestionId = userQuestion.Id
                };
            }
            catch (System.Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<CreateQuestionResult> CreateVoiceSubjectQuestion(CreateVoiceSubjectQuestionDto dto)
        {
            try
            {
                var questionCodePrefix = dto.TechnicianType == TechnicianType.Agent ? "AGN" : (dto.TechnicianType == TechnicianType.Expert ? "EXP" : "GUD");

                var userWallet = await _userWalletRepository.GetWithDebit(dto.UserId, dto.ProperPrice.PriceCurrencyId);

                var attachment =
                    dto.QuestionAttachments?
                        .Select(m => new UserQuestionAttachment(m.FileHttpAddress, m.FileId))
                        .ToList();

                var userQuestion = new UserQuestion(dto.QuestionTitle, new UserQuestionSubjectDetail(dto.SubjectTitle, dto.SubjectId), (dto.IsTextAnswer ? QuestionAnswerType.Text : QuestionAnswerType.Voice), dto.ProperPrice, dto.UserId
                    , questionCodePrefix, dto.Voice.FileHttpAddress, dto.Voice.FileId, userWallet, dto.TechnicianType, attachment);

                await _unitOfWork.BeginTransaction();
                await _userQuestionRepository.CreateUserQuestion(userQuestion);
                await _unitOfWork.CommitAsync();

                userWallet.DecreaseUserCredit(userQuestion.Id, dto.ProperPrice.UnitPrice, $"{userQuestion.QuestionCode()}");

                await _unitOfWork.CommitAsync();

                var user = await _userRepository.GetUserById(dto.UserId);

                //await _publisher.PublishAsync(new UserQuestionEventBuilder(userQuestion, dto.QuestionAttachments, user, dto.ProperPrice).BuildTextSubject());

                await _unitOfWork.CommitTransaction();

                return new CreateQuestionResult()
                {
                    QuestionCode = userQuestion.QuestionCode(),
                    QuestionId = userQuestion.Id
                };
            }
            catch (System.Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<CancelQuestionResult> CancelQuestion(CancelQuestionInput input)
        {
            try
            {
                UserQuestion userQuestion = await _userQuestionRepository.FindWithStates(input.QuestionId);
                if (userQuestion.UserId != input.UserId)
                {
                    throw new System.Exception("امکان کنسل کردن سوال وجود ندارد.");
                }
                var userWallet = await _userWalletRepository.GetWithDebitByQuestion(userQuestion.Id);

                await _unitOfWork.BeginTransaction();
                userQuestion.Cancel(userWallet);
                await _unitOfWork.CommitAsync();

             //   var user = await _userRepository.GetUserById(userId);

               // await _publisher.PublishAsync(new UserQuestionEventBuilder(userQuestion, dto.QuestionAttachments, user, dto.ProperPrice).BuildTextSubject());

                await _unitOfWork.CommitTransaction();

                return new CancelQuestionResult()
                {
                    QuestionCode = userQuestion.QuestionCode(),
                    QuestionId = userQuestion.Id
                };
            }
            catch (System.Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }

        }
    }
}