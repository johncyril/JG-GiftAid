using JG.FinTech.GiftAid.Calculator;
using NUnit.Framework;
using System;

namespace JG.FinTech.GiftAid.Calculator.Tests
{
    public class GiftAidCalculatorTests
    {
        [TestCase(100, 20, ExpectedResult = 25)]
        [TestCase(100, 40, ExpectedResult = 66.67)]
        [TestCase(100, 22.5, ExpectedResult = 29.03)]
        [TestCase(1, 22.5, ExpectedResult = 0.29)]
        public decimal GiftAidCalculatedCorrectly(decimal donationAmount, decimal taxRate)
        {
            // Arrange
            var calculatorUnderTest = new GiftAidCalculator(taxRate);

            // Act
            var result = calculatorUnderTest.Calculate(donationAmount);

            // Assert
            return result;
        }

        [TestCase(0)]
        [TestCase(100)]
        public void GiftAidCalculatedThrowsOnInvalidTaxRate(decimal taxRate)
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentException>(() => new GiftAidCalculator(taxRate), $"Tax rate provided:{taxRate} is not a valid rate out of 100.");
        }

        [TestCase(0, 20)]
        public void GiftAidCalculatedReturns0on0DonationAmount(decimal donationAmount, decimal taxRate)
        {
            // Arrange
            var calculatorUnderTest = new GiftAidCalculator(taxRate);

            // Act
            var result = calculatorUnderTest.Calculate(donationAmount);

            // Assert
            Assert.AreEqual(0, result);

        }
    }
}