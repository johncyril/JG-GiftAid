using FluentValidation;
using JG.FinTech.GiftAid.Api.Controllers;
using System;
using System.Text.RegularExpressions;

namespace JG.FinTech.GiftAid.Api.Validations
{
    public class GiftAidDeclarationValidator : AbstractValidator<GiftAidDeclaration>
    {
        private string postcodeRegex = @"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})";
        private string monetaryValue = @"^\d+.\d{0,2}$";

        public GiftAidDeclarationValidator()
        {
            RuleFor(x => x.PostCode).NotEmpty().WithMessage("Postcode cannot be blank");
            RuleFor(x => x.PostCode).Must(BeAValidPostCode).WithMessage("postcode specified is invalid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be blank");
            RuleFor(x => x.DonationAmount).GreaterThanOrEqualTo(1).WithMessage("Donation amount must be at least 1"); ;
            RuleFor(x => x.DonationAmount).Must(BeValidMonetaryValue).WithMessage("Donation amount must be a valid monetary value"); ;
        }

        private bool BeValidMonetaryValue(double amount)
        {
            return Regex.IsMatch(amount.ToString(), monetaryValue);
        }

        private bool BeAValidPostCode(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode))
                return false;

            return Regex.IsMatch(postcode, postcodeRegex);
        }
    }
}
