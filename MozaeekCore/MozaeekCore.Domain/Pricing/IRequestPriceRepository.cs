using System.Threading.Tasks;

namespace MozaeekCore.Domain.Pricing
{
    public interface IRequestPriceRepository
    {
        Task CreateRequestPrice(RequestPriceHeader requestPrice);
        Task<RequestPriceHeader> Find(long id);
        void Update(RequestPriceHeader requestPrice);
        void Delete(RequestPriceHeader requestPrice);
    }
}