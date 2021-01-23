using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Exceptions;
using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;


namespace JG.FinTech.GiftAid.Unit.Tests
{
    public class GiftAidControllerTests
    {
        private IGiftAidCalculator calculator { get; set; }
        private IDonationValidator validator { get; set; }

        private IGiftAidController controllerUnderTest { get; set; }

        [SetUp]
        public void Setup()
        {
            calculator = Substitute.For<IGiftAidCalculator>();
            validator = Substitute.For<IDonationValidator>();
            controllerUnderTest = new Api.Implementations.GiftAidController(calculator, validator);
        }

        [Test]
        public async Task MakesCallToGiftAidCalculator_Success()
        {
            // Arrange
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = true });
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(25);            

            // Act
            var controllerResponse = await controllerUnderTest.GiftaidAsync(100);

            // Assert
            var result = controllerResponse.Result as OkObjectResult;
            var response = result.Value as GiftAidResponse;
            Assert.AreEqual(response.GiftAidAmount, 25);
            calculator.Received().Calculate(100);
            validator.Received().Validate(100);
        }

        [Test]
        public async Task Throws_When_CallToGiftAidCalculator_Fails()
        {
            // Arrange
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = true });
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(x => throw new System.Exception());            

            // Act/Assert
            Assert.ThrowsAsync<GiftAidException>(() => controllerUnderTest.GiftaidAsync(100), "An error ocurred when calculating the GiftAid amount");
            calculator.Received().Calculate(100);
            validator.Received().Validate(100);
        }

        [Test]
        public async Task Returns_BadRequest_When_AmoutsAreInvalid()
        {
            // Arrange
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = false, ValidationError = "Validation Error"});
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(25);            

            // Act
            var controllerResponse = await controllerUnderTest.GiftaidAsync(100);

            // Assert
            validator.Received().Validate(100);
            calculator.DidNotReceive().Calculate(Arg.Any<decimal>());

            var result = controllerResponse.Result as BadRequestObjectResult;
            Assert.AreEqual("Validation Error", result.Value);            
            Assert.AreEqual(result.StatusCode, 400);            
        }
    }
}