using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Exceptions;
using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JG.FinTech.GiftAid.Api.Implementations
{
    /// <summary>
    /// Implementation of the GiftAidController. Requests are passed through to here from auto-generated code
    /// </summary>
    public class GiftAidController : IGiftAidController
    {
        private readonly IGiftAidCalculator _giftAidCalculator;
        private readonly IDonationValidator _giftAidDonationValidator;

        public GiftAidController(IGiftAidCalculator giftAidCalculator, IDonationValidator giftAidDonationvalidator)
        {
            _giftAidCalculator = giftAidCalculator;
            _giftAidDonationValidator = giftAidDonationvalidator;
        }

        public async Task<ActionResult<GiftAidResponse>> GiftaidAsync(double amount)
        {
            var donationAmount = (decimal) amount;
            var validaitonResult = _giftAidDonationValidator.Validate(donationAmount);
            if (!validaitonResult.IsSuccess)
            {
                return new BadRequestObjectResult(validaitonResult.ValidationError);
            }

            try
            {
                var calculatedGiftAid = _giftAidCalculator.Calculate(donationAmount);

                var response = new GiftAidResponse()
                {
                    DonationAmount = amount,
                    GiftAidAmount = (double)calculatedGiftAid
                };

                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                throw new GiftAidException($"An error ocurred when calculating the GiftAid amount. {Environment.NewLine} {e.Message}");
            }
        }
    }
}
