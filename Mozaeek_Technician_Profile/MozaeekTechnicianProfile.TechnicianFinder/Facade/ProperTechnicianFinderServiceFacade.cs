using System;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekTechnicianProfile.ApplicationService.Services;
using MozaeekTechnicianProfile.TechnicianFinder.Service;

namespace MozaeekTechnicianProfile.TechnicianFinder
{
    public interface IProperTechnicianFinderServiceFacade
    {
        Task FindTechnicianForWaitingQuestion();
    }
    public class ProperTechnicianFinderServiceFacade : IProperTechnicianFinderServiceFacade
    {
        private readonly IQuestionToProcessService _questionToProcessService;
        private readonly IProperTechnicianFinderService _properTechnicianFinderService;
        private readonly ILogger _logger;

        public ProperTechnicianFinderServiceFacade(IQuestionToProcessService questionToProcessService, IProperTechnicianFinderService properTechnicianFinderService, ILogger logger)
        {
            _questionToProcessService = questionToProcessService;
            _properTechnicianFinderService = properTechnicianFinderService;
            _logger = logger;
        }

        public async Task FindTechnicianForWaitingQuestion()
        {
            var uidLog = Guid.NewGuid();
            var questions = await _questionToProcessService.GetQuestionWaitingForTechnicians();
            _logger.DoLog(uidLog + "Start Call GetAllQuestionForTechnicianProcesses From UserProfile" + DateTime.Now.ToString("G"));

            foreach (var question in questions)
            {
                _logger.DoLog(uidLog + "foreach UpdateQuestionState" + DateTime.Now.ToString("G"));
                var ExpireLimit = question.CreateDate.AddMinutes(5);

                if (ExpireLimit < DateTime.Now)
                {
                    await _questionToProcessService.UpdateQuestionState(question.QuestionId, UserQuestionState.TechnicianNotFound);
                }
                else
                {
                    //var techResult = await _properTechnicianFinderService.FindProperTechnician(question);
                    //if (techResult == null)
                    //{
                        await _questionToProcessService.UpdateQuestionState(question.QuestionId, UserQuestionState.TechnicianNotFound);
                    //}
                    //else
                    //{
                    //    //ToDo: Call Technician...
                    //}
                }
                _logger.DoLog(uidLog + "foreach UpdateQuestionState End " + DateTime.Now.ToString("G"));
            }
        }
    }
}