using Autofac;
using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.Validations;
using JG.FinTech.GiftAid.Calculator;

namespace JG.FinTech.GiftAid.Api.IoC
{
    /// <summary>
    /// Autofac module for DI
    /// </summary>
    public class GiftAidModule : Module
    {
        private readonly decimal _donationMaxValue;
        private readonly decimal _donationMinValue;
        private decimal _giftAidTaxRate { get; set; }

        public GiftAidModule(decimal giftAidTaxRate, decimal donationMinValue, decimal donationMaxValue)
        {
            _giftAidTaxRate = giftAidTaxRate;            
            _donationMinValue = donationMinValue;
            _donationMaxValue = donationMaxValue;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Implementations.GiftAidController>().As<IGiftAidController>().InstancePerLifetimeScope();
            builder.Register(c => new GiftAidCalculator(_giftAidTaxRate)).As<IGiftAidCalculator>().InstancePerLifetimeScope();
            builder.Register(c => new DonationValidator(_donationMinValue, _donationMaxValue)).As<IDonationValidator>().InstancePerLifetimeScope();
        }
    }
}
