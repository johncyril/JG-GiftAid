namespace JG.FinTech.GiftAid.Api.Validations
{
    public interface IDonationValidator
    {
        ValidationResponse Validate(decimal amount);
    }
}