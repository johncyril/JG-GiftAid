using System;

namespace JG.FinTech.GiftAid.Calculator
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        /// <summary>
        /// Calculates the gift aid amount, rounded up to the penny.
        /// </summary>
        /// <param name="donationAmount"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        public decimal Calculate(decimal donationAmount, decimal taxRate)
        {
            if (taxRate >= 100 || taxRate == 0)
            {
                throw new ArgumentException($"Tax rate provided:{taxRate} is not a valid rate out of 100.");
            }

           //gift aid is calculated as [Donation Amount] *( [TaxRate] / (100 - [TaxRate]))
            var denominator = 100 - taxRate;
            var multiplier = taxRate / denominator;
            
            var giftAidAmount = donationAmount * multiplier;

            return Math.Round(giftAidAmount, 2, MidpointRounding.AwayFromZero);
            
        }
    }
}
