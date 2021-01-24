using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JG.FinTech.GiftAid.Api.Integration.Tests
{
    /// <summary>
    /// This is a duplicated test class helper
    /// </summary>
    public static class TestConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public static string GetTestDbConnectionString()
        {
            var configuration = GetIConfigurationRoot();
            return configuration.GetConnectionString("GiftAidTestDb");
        }
    }
}
