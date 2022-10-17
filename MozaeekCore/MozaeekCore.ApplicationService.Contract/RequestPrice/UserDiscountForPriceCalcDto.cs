using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UserDiscountForPriceCalcDto
    {
        public UserDiscountForPriceCalcDto(bool isPercent, int maximumDiscountAmount, int? amount)
        {
            IsPercent = isPercent;
            MaximumDiscountAmount = maximumDiscountAmount;
            if (MaximumDiscountAmount!=0 && Amount!=0 && Amount> MaximumDiscountAmount )
            {
                throw new UserFriendlyException("خطا در تخفیفات کاربر");
            }
            Amount = amount;
        }

        public bool IsPercent { get; set; }
        
        /// <summary>
        /// مثال 10 درصد تخفیف تا سقف 2 هزار تومان
        /// </summary>
        public int MaximumDiscountAmount { get; set; }

        public int? Amount { get; set; }
    }
}