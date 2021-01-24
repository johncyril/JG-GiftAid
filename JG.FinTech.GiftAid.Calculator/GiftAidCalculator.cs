using System;

namespace JG.FinTech.GiftAid.Calculator
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private decimal _taxRate { get; set; }

        public GiftAidCalculator(decimal taxRate)
        {
            if (taxRate >= 100 || taxRate == 0)
            {
                throw new ArgumentException($"Tax rate configured:{taxRate} is not a valid rate out of 100.");
            }

            _taxRate = taxRate;
        }

        /// <summary>
        /// Calculates the gift aid amount, rounded up to the penny.
        /// </summary>
        /// <param name="donationAmount"></param>        
        /// <returns></returns>
        public decimal Calculate(decimal donationAmount)
        {
           if (donationAmount == default(decimal))
            {
                return 0;
            }

           //gift aid is calculated as [Donation Amount] *( [TaxRate] / (100 - [TaxRate]))
            var denominator = 100 - _taxRate;
            var multiplier = _taxRate / denominator;
            
            var giftAidAmount = donationAmount * multiplier;

            return Math.Round(giftAidAmount, 2, MidpointRounding.AwayFromZero);
            
        }
    }
}
