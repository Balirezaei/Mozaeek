using System.Linq;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.Enum;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class RequestPriceProfile
    {
        public static RequestPriceGrid GetRequestPriceGrid(this RequestPriceQuery query)
        {
            return new RequestPriceGrid()
            {
                Title = query.Title,
                Id = query.Id,
                IsActive = query.IsActive,
                StartDate = query.StartDate,
            };
        }


        public static RequestPriceDto GetRequestPriceDto(this RequestPriceQuery query)
        {
            return new RequestPriceDto()
            {
                Id = query.Id,
                Title = query.Title,
                StartDate = query.StartDate,
                IsActive = query.IsActive,
                EndDate = query.EndDate,
                PriceAmount = query.PriceAmount,
                PriceUnitId = query.PriceCurrencyId,
                SystemShare = query.SystemShare,
                PriceDetails = query.RequestPriceDetails.Select(m => new RequestPriceDetailDto
                {
                    RequestTitle = m.RequestTitle,
                    RequestId = m.RequestId
                }).ToList()
            };
        }

        public static ProperPriceRequestQuestion GetProperRequestPriceResult(this RequestPriceQuery query, string requestTitle, bool isOnline)
        {
            var price = new ProperPriceCalculation(query.PriceAmount, query.SystemShare, query.TechnicianShare, query.PriceCurrencyId, query.PriceCurrencyTitle);
            var technicianType = isOnline ? TechnicianType.Agent : TechnicianType.Expert;
            return new ProperPriceRequestQuestion(requestTitle, technicianType, price.GetProperPriceResult());
        }
    }
}