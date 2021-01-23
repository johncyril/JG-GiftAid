using Autofac;
using JG.FinTech.GiftAid.Api.Controllers;
using JG.FinTech.GiftAid.Api.IoC;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace JG.FinTech.GiftAid.Api.Integration.Tests
{
    public class Tests
    {
        [Test]
        public void ResolvesAllControllers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new GiftAidModule(20));
            var container = builder.Build();

            var controllers = Assembly.GetAssembly(typeof(GiftAidController)).GetTypes().Where(t => t.IsInterface && t.Name.EndsWith("Controller"));
            {
                foreach (var controller in controllers)
                {
                    var resolved = container.Resolve(controller);
                    if (resolved == null)
                    {
                        Assert.Fail($"Could not resolve {controller.Name}");
                    }
                }
            }
        }
    }
}