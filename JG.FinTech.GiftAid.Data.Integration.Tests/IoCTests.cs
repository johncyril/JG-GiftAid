using Autofac;
using JG.FinTech.GiftAid.Data.IoC;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;


namespace JG.FinTech.GiftAid.Data.Integration.Tests
{
    public class IoCTests
    {
        [TestCase]
        public void Repository_Resolves_Correctly()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DataModule(TestConfigurationHelper.GetTestDbConnectionString()));
            var container = builder.Build();

            var repository = container.Resolve<IRepository>();
            Assert.IsNotNull(repository);
        }
    }
}
