using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Exceptions;
using JG.FinTech.GiftAid.Calculator;
using System;
using System.Threading.Tasks;

namespace JG.FinTech.GiftAid.Api.Implementations
{
    public class GiftAidController : IGiftAidController
    {
        private IGiftAidCalculator _giftAidCalculator;

        public GiftAidController(IGiftAidCalculator giftAidCalculator)
        {
            _giftAidCalculator = giftAidCalculator;
        }

        public Task<GiftAidResponse> GiftaidAsync(double amount)
        {
            try
            {
                var calculatedGiftAid = _giftAidCalculator.Calculate((decimal)amount);

                var response = new GiftAidResponse()
                {
                    DonationAmount = amount,
                    GiftAidAmount = (double)calculatedGiftAid
                };

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                throw new GiftAidException($"An error ocurred when calculating the GiftAid amount. {Environment.NewLine} {e.Message}");
            }
        }
    }
}
