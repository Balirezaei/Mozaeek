using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karmizban.Support.Domain
{
    public interface IUserRequestSupportAnswerRepository
    {
        IQueryable<UserRequestSupportAnswer> GetAll();
    }
}