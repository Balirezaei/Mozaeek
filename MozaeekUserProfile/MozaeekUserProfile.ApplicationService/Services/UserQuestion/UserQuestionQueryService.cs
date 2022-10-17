using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mozaeek.CR.PublicDto;
using MozaeekUserProfile.ApplicationService.Contract;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.ApplicationService.Services
{

    public interface IUserQuestionQueryService
    {
        Task<List<UserQuestionHistory>> GetAllUserQuestion(UserQuestionHistoryQuery contract);
        Task<ActiveUserQuestion> GetActiveUserQuestion(long userId);
        Task<OpenUserQuestion> GetOpenUserQuestion(long userId);
        Task<List<UserQuestionHistory>> GetRecentUserQuestion(UserQuestionHistoryQuery contract);
    }

    public class UserQuestionQueryService : IUserQuestionQueryService
    {
        private readonly IUserQuestionRepository _userQuestionRepository;

        public UserQuestionQueryService(IUserQuestionRepository userQuestionRepository)
        {
            _userQuestionRepository = userQuestionRepository;
        }

        public async Task<List<UserQuestionHistory>> GetAllUserQuestion(UserQuestionHistoryQuery contract)
        {
            return (await _userQuestionRepository.GetAll(m => m.UserId == contract.UserId && m.LastQuestionState==UserQuestionState.AnsweredByTechnician)
                    .Skip((contract.PageNumber - 1) * contract.PageSize)
                    .Take(contract.PageSize)
                    .ToListAsync())
                .Select(m => new UserQuestionHistory
                {
                    CreateDate = m.CreateDate.ToString("D"),
                    QuestionCode = m.QuestionCode(),
                    QuestionId = m.Id,
                    QuestionSubject = m.RequestTitle != null ? m.RequestTitle : m.SubjectTitle,
                    TechnicianCode = "",
                    QuestionStateDescription = "کارشناس یافت نشد"
                }).ToList();
        }

        public async Task<ActiveUserQuestion> GetActiveUserQuestion(long userId)
        {
            var list = await _userQuestionRepository.GetAll(m => m.UserId == userId && (int)m.LastQuestionState == 0)
                .ToListAsync();
            return new ActiveUserQuestion() { RecentlyReceivedAnswer = 0, WaitingForAnswer = 0 };
        }

        public async Task<OpenUserQuestion> GetOpenUserQuestion(long userId)
        {
            var question = await _userQuestionRepository.GetAll(m => m.UserId == userId && m.LastQuestionState == UserQuestionState.SendByUser)
                  .FirstOrDefaultAsync();
            if (question != null && question.CreateDate.AddMinutes(5) < DateTime.Now)
            {
                return new OpenUserQuestion()
                {
                    QuestionTitle = question.QuestionTitle,
                    QuestionId = question.Id
                };
            }

            return null;
        }

        public async Task<List<UserQuestionHistory>> GetRecentUserQuestion(UserQuestionHistoryQuery contract)
        {
            return (await _userQuestionRepository.GetAll(m => m.UserId == contract.UserId)
                    .OrderByDescending(m=>m.Id)
                    .Skip((contract.PageNumber - 1) * contract.PageSize)
                    .Take(contract.PageSize)
                    .ToListAsync())
                .Select(m => new UserQuestionHistory
                {
                    CreateDate = m.CreateDate.ToString("D"),
                    QuestionCode = m.QuestionCode(),
                    QuestionId = m.Id,
                    QuestionSubject = m.RequestTitle != null ? m.RequestTitle : m.SubjectTitle,
                    TechnicianCode = "",
                }).ToList();
        }
    }
}