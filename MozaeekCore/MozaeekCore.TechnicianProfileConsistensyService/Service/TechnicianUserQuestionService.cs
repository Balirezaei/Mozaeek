using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.TechnicianProfileConsistensyService.Context;
using MozaeekCore.TechnicianProfileConsistensyService.Model;

namespace MozaeekCore.TechnicianProfileConsistensyService.Service
{
    public interface ITechnicianUserQuestionService
    {
        Task<UserQuestionWaitingForTechnician> CreateTechnicianUserQuestion(UserQuestionWaitingForTechnician userQ);
    }

    public class TechnicianUserQuestionService : ITechnicianUserQuestionService
    {
        private readonly TechnicianProfileContext _context;

        public TechnicianUserQuestionService(TechnicianProfileContext context)
        {
            _context = context;
        }

        public async Task<UserQuestionWaitingForTechnician> CreateTechnicianUserQuestion(UserQuestionWaitingForTechnician userQ)
        {
            var exist = await _context.UserQuestionWaitingForTechnicians.AnyAsync(m => m.QuestionId == userQ.QuestionId);
            if (!exist)
            {
                await _context.UserQuestionWaitingForTechnicians.AddAsync(userQ);
                await _context.SaveChangesAsync();
            }
            return userQ;
        }
    }
}