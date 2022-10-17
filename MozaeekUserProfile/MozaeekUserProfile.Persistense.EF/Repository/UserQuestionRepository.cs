using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.Repository
{
    public class UserQuestionRepository : IUserQuestionRepository
    {
        private readonly MozaeekUserProfileContext _context;

        public UserQuestionRepository(MozaeekUserProfileContext context)
        {
            _context = context;
        }

        public async Task CreateUserQuestion(UserQuestion userQuestion)
        {
            await _context.UserQuestions.AddAsync(userQuestion);
        }

        public IQueryable<UserQuestion> GetAll(Expression<Func<UserQuestion, bool>> predicate)
        {
            return _context.UserQuestions.Where(predicate);
        }

        public Task<UserQuestion> FindWithStates(long id)
        {
            return _context.UserQuestions
                .Include(m=>m.QuestionStates)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}