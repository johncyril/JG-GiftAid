using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTech.GiftAid.Api.Validations
{
    public class DonationValidator : IDonationValidator
    {
        private readonly decimal maximumValue;
        private readonly decimal minimumValue;

        public DonationValidator(decimal minimumValue, decimal maximumValue)
        {
            this.maximumValue = maximumValue;
            this.minimumValue = minimumValue;
        }

        public ValidationResponse Validate(decimal amount)
        {
            if (amount < minimumValue || amount > maximumValue)
            {
                return new ValidationResponse
                {
                    IsSuccess = false,
                    ValidationError = $"Donation amount:{amount} must be greater than {minimumValue} and lower than {maximumValue}"
                };
            }

            else
            {
                return new ValidationResponse
                {
                    IsSuccess = true
                };
            }

        }
    }
}
