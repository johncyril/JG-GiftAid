using FluentValidation.TestHelper;
using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Validations;
using NUnit.Framework;

namespace JG.FinTech.GiftAid.Api.Unit.Tests
{
    public class GiftAidDeclarationValidatorTests
    {
        private GiftAidDeclarationValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new GiftAidDeclarationValidator();
        }

        [TestCase(null, false)]
        [TestCase("11111111", false)]
        [TestCase("E111 889J", false)]
        [TestCase("EC2A2DB", false)]
        [TestCase("EC2A 2DB", true)]
        [TestCase("TW4 5BJ", true)]
        [TestCase("E14 8HL", true)]
        public void Errors_When_Postcode_invalid(string postCode, bool expectedPass)
        {
            var model = new GiftAidDeclaration 
            { 
                Name = "Joe Bloggs", DonationAmount = 100, PostCode = postCode 
            };
            var result = validator.TestValidate(model);

            if (expectedPass)
            {
                result.ShouldNotHaveValidationErrorFor(d => d.PostCode);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(d => d.PostCode);
            }
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        public void Errors_When_Name_Empty(string name)
        {
            var model = new GiftAidDeclaration
            {
                Name = name,
                DonationAmount = 100,
                PostCode = "E15 2JD"
            };
            var result = validator.TestValidate(model);

           result.ShouldHaveValidationErrorFor(d => d.Name);           
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(0.1)]
        [TestCase(0.13)]        
        [TestCase(0.99)]                
        public void Errors_When_DonationAmount_LT_1(double amount)
        {
            var model = new GiftAidDeclaration
            {
                Name = "Joe Bloggs",
                DonationAmount = amount,
                PostCode = "E15 2JD"
            };
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(d => d.DonationAmount);
        }


        [TestCase(12.13, true)]
        [TestCase(12.00, true)]
        [TestCase(120.11, true)]
        [TestCase(2000.00, true)]
        [TestCase(12.1234, false)]
        [TestCase(12.199, false)]
        [TestCase(1200.199, false)]
        [TestCase(1200.9889, false)]
        [TestCase(1.00, false)]
        public void Errors_When_DonationAmount_More_Than_2DP(double amount, bool expectedPass)
        {
            var model = new GiftAidDeclaration
            {
                Name = "Joe Bloggs",
                DonationAmount = amount,
                PostCode = "E15 2JD"
            };
            var result = validator.TestValidate(model);

            if (expectedPass)
            {
                result.ShouldNotHaveValidationErrorFor(d => d.DonationAmount);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(d => d.DonationAmount);
            }
        }
    }
}
