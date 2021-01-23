namespace JG.FinTech.GiftAid.Calculator
{
    public interface IGiftAidCalculator
    {
        /// <summary>
        /// Calculates the gift aid amount based on donation amount and tax rate
        /// </summary>
        /// <param name="donationAmount"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        decimal Calculate(decimal donationAmount, decimal taxRate);
    }
}