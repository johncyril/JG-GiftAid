using JG.FinTech.GiftAid.Data.Entities;
using LiteDB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace JG.FinTech.GiftAid.Data.Integration.Tests
{

    /// <summary>
    /// A simple integration test to check LiteDb works
    /// </summary>
    public class LiteDbTests
    {
        private IRepository _repositoryUnderTest;
        private ILiteRepository _liteRepository;
        private Declaration testDeclaration;

        [SetUp]
        public void Setup()
        {
            _liteRepository = new LiteRepository(new LiteDatabase(TestConfigurationHelper.GetTestDbConnectionString()));
            _repositoryUnderTest = new Repository(_liteRepository);

            _liteRepository.Database.DropCollection("Declaration");

            testDeclaration = new Declaration()
            {
                DonationAmount = 100,
                Name = "Joe Bloggs",
                PostCode = "EC2A 2DB"
            };
        }

        [Test]
        public void Declaration_Peristence_Success()
        {
            // Act
            _repositoryUnderTest.SaveDeclaration(testDeclaration);

            // Assert
            Assert.AreEqual(testDeclaration.DeclarationId, _liteRepository.Single<Declaration>(x => x.DeclarationId == testDeclaration.DeclarationId).DeclarationId);             
        }       
    }
}