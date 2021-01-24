using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;
using JG.FinTech.GiftAid.Data;
using JG.FinTech.GiftAid.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JG.FinTech.GiftAid.Api.Controllers
{
    /// <summary>
    /// Implementation of the GiftAidController. Requests are passed through to here from auto-generated code
    /// </summary>
    [ApiController]
    public class GiftAidController : GiftAidControllerBase
    {
        private readonly IGiftAidCalculator _giftAidCalculator;
        private readonly IDonationValidator _giftAidDonationValidator;
        private readonly IRepository _repository;

        public GiftAidController(IGiftAidCalculator giftAidCalculator, IDonationValidator giftAidDonationvalidator, IRepository repository)
        {
            _giftAidCalculator = giftAidCalculator;
            _giftAidDonationValidator = giftAidDonationvalidator;
            _repository = repository;
        }

        public async override Task<ActionResult<GiftAidDeclarationResponse>> Declarations([FromBody] GiftAidDeclaration declaration)
        {
            try
            {
                // TODO - With fluent validation from the start, I could have kept these controllers more DRY.
                var donationAmount = (decimal)declaration.DonationAmount;
                var validationResult = _giftAidDonationValidator.Validate(donationAmount);
                if (!validationResult.IsSuccess)
                {
                    return new BadRequestObjectResult(validationResult.ValidationError);
                }


                // TODO - As mentioned in the submission readme, I don't love the direct use of our repository and db entities here. As outlined, with more time, I would have written a better abstraction
                var declarationToSave = new Declaration
                {
                    DonationAmount = donationAmount,
                    Name = declaration.Name,
                    PostCode = declaration.PostCode
                };

                _repository.SaveDeclaration(declarationToSave);

                var giftAidAmount = _giftAidCalculator.Calculate((decimal)declaration.DonationAmount);

                var response = new GiftAidDeclarationResponse
                {
                    GiftAidAmount = (double)giftAidAmount,
                    Token = declarationToSave.DeclarationId.ToString()
                };

                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status201Created
                };

            }
            catch (Exception e)
            {
                return new ObjectResult($"An error ocurred while saving the declaration. {Environment.NewLine} {e.Message}")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async override Task<ActionResult<GiftAidResponse>> Giftaid([FromQuery] double amount)
        {
            var donationAmount = (decimal)amount;
            var validationResult = _giftAidDonationValidator.Validate(donationAmount);
            if (!validationResult.IsSuccess)
            {
                return new BadRequestObjectResult(validationResult.ValidationError);
            }

            try
            {
                var calculatedGiftAid = _giftAidCalculator.Calculate(donationAmount);

                var response = new GiftAidResponse()
                {
                    DonationAmount = amount,
                    GiftAidAmount = (double)calculatedGiftAid
                };

                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                return new ObjectResult($"An error ocurred when calculating the GiftAid amount. { Environment.NewLine } { e.Message}")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
