namespace JG.FinTech.GiftAid.Api.Validations
{
    /// <summary>
    /// Validaiton response object 
    /// </summary>
    public class ValidationResponse
    {
        public bool IsSuccess { get; set; }

        public string ValidationError { get; set; }
    }
}