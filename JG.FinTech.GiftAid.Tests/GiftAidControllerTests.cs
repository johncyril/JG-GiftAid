using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Exceptions;
using JG.FinTech.GiftAid.Calculator;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace JG.FinTech.GiftAid.Unit.Tests
{
    public class GiftAidControllerTests
    {
        private IGiftAidCalculator calculator { get; set; }

        [SetUp]
        public void Setup()
        {
            calculator = Substitute.For<IGiftAidCalculator>();
        }

        [Test]
        public async Task MakesCallToGiftAidCalculator_Success()
        {
            // Arrange
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(25);
            IGiftAidController controllerUnderTest = new Api.Implementations.GiftAidController(calculator);

            // Act
            var response = await controllerUnderTest.GiftaidAsync(100);

            // Assert
            Assert.AreEqual(response.GiftAidAmount, 25);
            calculator.Received().Calculate(100);
        }

        [Test]
        public async Task Throws_When_CallToGiftAidCalculator_Fails()
        {
            // Arrange
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(x => throw new System.Exception());
            IGiftAidController controllerUnderTest = new Api.Implementations.GiftAidController(calculator);

            // Act/Assert
            Assert.ThrowsAsync<GiftAidException>(() => controllerUnderTest.GiftaidAsync(100), "An error ocurred when calculating the GiftAid amount");
            calculator.Received().Calculate(100);
        }
    }
}