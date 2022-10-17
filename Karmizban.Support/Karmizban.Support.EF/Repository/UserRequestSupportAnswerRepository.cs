using System.Linq;
using Karmizban.Support.Domain;
using Karmizban.Support.EF.ContextContainer;

namespace Karmizban.Support.EF.Repository
{
    public class UserRequestSupportAnswerRepository : IUserRequestSupportAnswerRepository
    {
        private readonly SupportContext _context;

        public UserRequestSupportAnswerRepository(SupportContext context)
        {
            _context = context;
        }


        public IQueryable<UserRequestSupportAnswer> GetAll()
        {
            return _context.UserRequestSupportAnswers;
        }
    }
}