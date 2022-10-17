using System.Linq;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Mapper
{
    public static class SubjectPriceProfile
    {
        public static SubjectPriceGrid GetSubjectPriceGrid(this SubjectPriceQuery query)
        {
            return new SubjectPriceGrid()
            {
                Title = query.Title,
                Id = query.Id,
                IsActive = query.IsActive,
                StartDate = query.StartDate,
            };
        }

        public static SubjectPriceDto GetSubjectPriceDto(this SubjectPriceQuery query)
        {
            return new SubjectPriceDto()
            {
                Id = query.Id,
                Title = query.Title,
                StartDate = query.StartDate,
                IsActive = query.IsActive,
                EndDate = query.EndDate,
                PriceAmount = query.PriceAmount,
                PriceCurrencyId = query.PriceCurrencyId,
                SystemShare = query.SystemShare,
                PriceDetails = query.SubjectPriceDetails.Select(m => new SubjectPriceDetailDto
                {
                    SubjectTitle = m.SubjectTitle,
                    SubjectId = m.SubjectId
                }).ToList()
            };
        }

        public static ProperPriceSubjectQuestion GetProperSubjectPriceResult(this SubjectPriceQuery query, string subjectTitle)
        {
            var properRequestPrice = new ProperPriceCalculation(query.PriceAmount, query.SystemShare, query.TechnicianShare, query.PriceCurrencyId, query.PriceCurrencyTitle);
            return new ProperPriceSubjectQuestion(subjectTitle, TechnicianType.Guide, properRequestPrice.GetProperPriceResult()); ;
        }

        public static ProperPriceResult GetProperPriceResult(this ProperPriceCalculation pc)
        {
            return new ProperPriceResult()
            {
                TechnicianShare = pc.TechnicianShare,
                SystemShare = pc.SystemShare,
                PriceCurrencyTitle = pc.PriceCurrencyTitle,
                SystemPercent = pc.SystemPercent,
                TechnicianPercent = pc.TechnicianPercent,
                UnitPrice = pc.UnitPrice,
                PriceCurrencyId = pc.PriceCurrencyId
            };
        }

    }
}