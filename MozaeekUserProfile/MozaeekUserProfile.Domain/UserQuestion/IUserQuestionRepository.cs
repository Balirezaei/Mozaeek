using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Domain
{
    public interface IUserQuestionRepository
    {
        Task CreateUserQuestion(UserQuestion userQuestion);
        IQueryable<UserQuestion> GetAll(Expression<Func<UserQuestion, bool>> predicate);

        Task<UserQuestion> FindWithStates(long id);
    }
}