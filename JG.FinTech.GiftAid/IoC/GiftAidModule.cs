using Autofac;
using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Calculator;

namespace JG.FinTech.GiftAid.Api.IoC
{
    public class GiftAidModule : Module
    {
        private decimal _giftAidTaxRate { get; set; }

        public GiftAidModule(decimal giftAidTaxRate)
        {
            _giftAidTaxRate = giftAidTaxRate;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Implementations.GiftAidController>().As<IGiftAidController>().InstancePerLifetimeScope();
            builder.Register(c => new GiftAidCalculator(_giftAidTaxRate)).As<IGiftAidCalculator>().InstancePerLifetimeScope();
        }
    }
}
