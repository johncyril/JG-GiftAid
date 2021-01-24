using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;
using JG.FinTech.GiftAid.Data;
using JG.FinTech.GiftAid.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;


namespace JG.FinTech.GiftAid.Unit.Tests
{
    public class GiftAidDeclarationTests
    {
        private IGiftAidCalculator calculator { get; set; }
        private IDonationValidator validator { get; set; }
        private IRepository repository { get; set; }
        private GiftAidController controllerUnderTest { get; set; }

        private GiftAidDeclaration testRequest;

        [SetUp]
        public void Setup()
        {
            calculator = Substitute.For<IGiftAidCalculator>();
            validator = Substitute.For<IDonationValidator>();
            repository = Substitute.For<IRepository>();
            controllerUnderTest = new GiftAidController(calculator, validator, repository);

            testRequest = new GiftAidDeclaration
            {
                Name = "Joe Bloggs",
                DonationAmount = 100,
                PostCode = "E14 8JF",
            };
        }

        [Test]
        public async Task MakesCallToRepository_Success()
        {
            // Arrange
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(25);
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = true });

            // Act
            var controllerResponse = await controllerUnderTest.Declarations(testRequest);

            // Assert
            var result = controllerResponse.Result as ObjectResult;
            var response = result.Value as GiftAidDeclarationResponse;
            Assert.AreEqual(response.GiftAidAmount, 25);
            calculator.Received().Calculate(100);
            validator.Received().Validate(100);
            repository.Received().SaveDeclaration(Arg.Is<Declaration>(x => x.DonationAmount == 100 && x.PostCode == "E14 8JF"));
        }

        [Test]
        public async Task BadRequest_When_CallToValidator_Fails()
        {
            // Arrange
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = false, ValidationError = "Validation Error" });

            // Act
            var controllerResponse = await controllerUnderTest.Declarations(testRequest);

            // Assert
            validator.Received().Validate(100);
            calculator.DidNotReceive().Calculate(Arg.Any<decimal>());
            repository.DidNotReceive().SaveDeclaration(Arg.Any<Declaration>());

            var result = controllerResponse.Result as BadRequestObjectResult;
            Assert.AreEqual("Validation Error", result.Value);
            Assert.AreEqual(result.StatusCode, 400);           
        }

        [Test]
        public async Task Returns_ErrorResponse_When_Persistence_Fails()
        {
            // Arrange
            calculator.Calculate(Arg.Any<decimal>()).ReturnsForAnyArgs(25);
            validator.Validate(Arg.Any<decimal>()).ReturnsForAnyArgs(new ValidationResponse { IsSuccess = true });
            repository.When(x => x.SaveDeclaration(Arg.Any<Declaration>())).Do(x => { throw new DatabaseException("Could Not Insert Declaration"); });

            // Act
            var controllerResponse = await controllerUnderTest.Declarations(testRequest);

            // Assert
            validator.Received().Validate(100);
            repository.Received().SaveDeclaration(Arg.Is<Declaration>(x => x.DonationAmount == 100 && x.PostCode == "E14 8JF"));
            calculator.DidNotReceive().Calculate(Arg.Any<decimal>());

            var result = controllerResponse.Result as ObjectResult;
            Assert.AreEqual("An error ocurred while saving the declaration. \r\n Could Not Insert Declaration", result.Value.ToString());            
        }
    }
}