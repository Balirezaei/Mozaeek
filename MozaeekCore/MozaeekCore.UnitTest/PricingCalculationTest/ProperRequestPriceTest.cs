using System.Collections.Generic;
using FluentAssertions;
using MozaeekCore.ApplicationService.Contract;
using Xunit;

namespace MozaeekCore.UnitTest.PricingCalculationTest
{
    public class ProperRequestPriceTest
    {
        private List<UserDiscountForPriceCalcDto> userDiscounts = new List<UserDiscountForPriceCalcDto>()
        {
            new UserDiscountForPriceCalcDto(true,0,30),
           // new UserDiscountForPriceCalcDto(true,2000,30),

        };

        [Theory]
        [InlineData(10000, 12, 88, 1200, 8800, 10000)]
        [InlineData(11000, 13, 87, 1430, 9570, 11000)]
        [InlineData(110, 19, 81, 21, 89, 110)]
        public void System_shoulde_Return_Correct_Amount_Price_Without_Dicount(int unitPrice, int systemShare, int technicianShare, int expectedSystemShare, int expectedTechnicianShre, int expectedUserShare)
        {
            var priceRequest = new ProperPriceCalculation(unitPrice, systemShare, technicianShare, 0, "");
            priceRequest.SystemShare.Should().Be(expectedSystemShare);
            priceRequest.TechnicianShare.Should().Be(expectedTechnicianShre);
        }

        //ToDO: think about user discount
        // [Theory]
        // [InlineData(10000, 12, 88, 840, 8800, 7000, 1)]
        // // [InlineData(11000, 13, 87, 1430, 9570, 11000)]
        // // [InlineData(110, 19, 81, 21, 89, 110)]
        // public void System_shoulde_Return_Correct_Amount_Price_With_Simple_UserDicount(int unitPrice, int systemShare, int technicianShare, int expectedSystemShare, int expectedTechnicianShre, int expectedUserShare, int discountIndex)
        // {
        //     var priceRequest = new ProperRequestPriceResult(unitPrice, userDiscounts[discountIndex], systemShare, technicianShare, 0, 0);
        //     priceRequest.SystemShare.Should().Be(expectedSystemShare);
        //     priceRequest.TechnicianShare.Should().Be(expectedTechnicianShre);
        //     priceRequest.UserFinalShare.Should().Be(expectedUserShare);
        // }
    }
}