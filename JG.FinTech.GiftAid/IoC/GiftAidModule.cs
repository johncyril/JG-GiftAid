using Autofac;
using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;
using JG.FinTech.GiftAid.Data.IoC;

namespace JG.FinTech.GiftAid.Api.IoC
{
    /// <summary>
    /// Autofac module for DI
    /// </summary>
    public class GiftAidModule : Module
    {
        private readonly decimal _donationMaxValue;
        private readonly decimal _donationMinValue;
        private readonly decimal _giftAidTaxRate;
        private readonly string _databaseConnection;

        public GiftAidModule(decimal giftAidTaxRate, decimal donationMinValue, decimal donationMaxValue, string databaseConnection)
        {
            _giftAidTaxRate = giftAidTaxRate;            
            _donationMinValue = donationMinValue;
            _donationMaxValue = donationMaxValue;
            _databaseConnection = databaseConnection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataModule(_databaseConnection));            
            builder.Register(c => new GiftAidCalculator(_giftAidTaxRate)).As<IGiftAidCalculator>().InstancePerLifetimeScope();
            builder.Register(c => new DonationValidator(_donationMinValue, _donationMaxValue)).As<IDonationValidator>().InstancePerLifetimeScope();
        }
    }
}
