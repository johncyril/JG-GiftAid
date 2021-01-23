using JG.FinTech.GiftAid.Calculator;
using NUnit.Framework;
using System;

namespace JG.Fintech.GiftAid.Calculator.Tests
{
    public class GiftAidCalculatorTests
    {
        [TestCase(100, 20, ExpectedResult = 25)]
        [TestCase(100, 40, ExpectedResult = 66.67)]        
        [TestCase(100, 22.5, ExpectedResult = 29.03)]
        public decimal GiftAidCalculatedCorrectly(decimal donationAmount, decimal taxRate)
        {
            // Arrange
            var calculatorUnderTest = new GiftAidCalculator();

            // Act
            var result = calculatorUnderTest.Calculate(donationAmount, taxRate);

            // Assert
            return result;
        }

        [TestCase(100, 0)]
        [TestCase(100, 100)]
        public void GiftAidCalculatedThrowsOnInvalidTaxRate(decimal donationAmount, decimal taxRate)
        {
            // Arrange
            var calculatorUnderTest = new GiftAidCalculator();

            // Act/Assert
            Assert.Throws<ArgumentException>(()=> calculatorUnderTest.Calculate(donationAmount, taxRate), $"Tax rate provided:{taxRate} is not a valid rate out of 100.");                       
        }

        [TestCase(100, 0)]
        [TestCase(100, 100)]
        public void GiftAidCalculatedThrowsOnInvalidDonationAmount(decimal donationAmount, decimal taxRate)
        {
            // Arrange
            var calculatorUnderTest = new GiftAidCalculator();

            // Act/Assert
            Assert.Throws<ArgumentException>(() => calculatorUnderTest.Calculate(donationAmount, taxRate), $"Tax rate provided:{taxRate} is not a valid rate out of 100.");
        }
    }
}