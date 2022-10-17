using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Mozaeek.CR.PublicDto;
using MozaeekTechnicianProfile.TechnicianFinder.Service;
using MozaeekTechnicianProfile.UserQuestionProgress;

namespace MozaeekTechnicianProfile.TechnicianFinder
{
    public interface IQuestionToProcessService
    {
        Task<List<UserQuestionForTechnicianProcess>> GetQuestionWaitingForTechnicians();
        Task UpdateQuestionState(long questionId,UserQuestionState state);
    }
    public class QuestionToProcessService : IQuestionToProcessService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IQuestionWorkflowService _questionWorkflowService;
        private readonly ILogger _logger;

        public QuestionToProcessService(IMemoryCache memoryCache, IQuestionWorkflowService questionWorkflowService, ILogger logger)
        {
            _memoryCache = memoryCache;
            _questionWorkflowService = questionWorkflowService;
            _logger = logger;
        }
        
        public async Task<List<UserQuestionForTechnicianProcess>> GetQuestionWaitingForTechnicians()
        {
            //_logger.DoLog("before await _questionWorkflowService.GetAllQuestionForTechnicianProcesses()");
            var result = await _questionWorkflowService.GetAllQuestionForTechnicianProcesses();
            //_logger.DoLog("after await _questionWorkflowService.GetAllQuestionForTechnicianProcesses()");
            if (result!=null)
            {
                ExcludePreRetrived(result);
            }
        
            //_logger.DoLog("after ExcludePreRetrived()");
            return result;
        }

        public Task UpdateQuestionState(long questionId, UserQuestionState state)
        {
            return _questionWorkflowService.UpdateQuestionState(new UpdateUserQuestionStateInput()
                {QuestionId = questionId, State = state});
        }

        public void ExcludePreRetrived(List<UserQuestionForTechnicianProcess> list)
        {
            List<UserQuestionForTechnicianProcess> preSaved = null;
            _memoryCache.TryGetValue("QuestionWaitingForTechnicians", out preSaved);
            if (preSaved != null)
            {
                foreach (var question in preSaved)
                {
                    var repetitive = list.FirstOrDefault(m => m.QuestionId == question.QuestionId);
                    list.Remove(repetitive);
                }
            }
            else
            {
                _memoryCache.Set("QuestionWaitingForTechnicians", list, DateTime.UtcNow.AddHours(1));
            }
        }
    }

}