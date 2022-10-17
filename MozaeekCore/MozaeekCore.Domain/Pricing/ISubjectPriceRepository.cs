using System.Threading.Tasks;

namespace MozaeekCore.Domain.Pricing
{
    public interface ISubjectPriceRepository
    {
        Task CreateSubjectPrice(SubjectPriceHeader subjectPrice);
        Task<SubjectPriceHeader> Find(long id);
        void Update(SubjectPriceHeader subjectPrice);
        void Delete(SubjectPriceHeader subjectPrice);
    }
}