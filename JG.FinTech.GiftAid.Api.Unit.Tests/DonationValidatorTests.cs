using JG.FinTech.GiftAid.Api.Validations;
using NUnit.Framework;

namespace JG.FinTech.GiftAid.Api.Unit.Tests
{
    public class DonationValidatorTests
    {
        private IDonationValidator validatorUnderTest;        
        private decimal maxValue = 100000;
        private decimal minValue = 2;

        [SetUp]
        public void SetUp()
        {            
            validatorUnderTest = new DonationValidator(minValue, maxValue);
        }

        [TestCase(2, true)]
        [TestCase(100000, true)]
        [TestCase(100, true)]
        [TestCase(100.28, true)]
        [TestCase(100000.1, false)]
        [TestCase(1.99, false)]
        [TestCase(5000000, false)]        
        public void Invalidates_InvalidAmounts(decimal donationAmount, bool expectedSuccess)
        {
            // Arrange

            // Act
            var result = validatorUnderTest.Validate(donationAmount);

            // Assert
            Assert.AreEqual(expectedSuccess, result.IsSuccess);
            if (!expectedSuccess)
            {
                Assert.AreEqual($"Donation amount:{donationAmount} must be greater than {minValue} and lower than {maxValue}", result.ValidationError);
            }           
        }
    }
}
