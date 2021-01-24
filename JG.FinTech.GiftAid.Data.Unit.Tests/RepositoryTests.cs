using LiteDB;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using Neleus.LambdaCompare;
using JG.FinTech.GiftAid.Data.Entities;

namespace JG.FinTech.GiftAid.Data.Unit.Tests
{
    public class RepositoryTests
    {
        private ILiteRepository _liteDbRepository;
        private IRepository _repositoryUnderTest;

        private Declaration testDeclaration;

        [SetUp]
        public void Setup()
        {
            _liteDbRepository = Substitute.For<ILiteRepository>();
            _repositoryUnderTest = new Repository(_liteDbRepository);

            testDeclaration = new Declaration()
            {
                DeclarationId = Guid.NewGuid(),
                DonationAmount = 100,
                Name = "Joe Bloggs",
                PostCode = "EC2A 2DB"
            };
        }

        [Test]
        public void Declaration_Peristence_Success()
        {
            // Arrange
            _liteDbRepository.SingleById<Declaration>(testDeclaration.DeclarationId).Returns(testDeclaration);

            // Act
            _repositoryUnderTest.SaveDeclaration(testDeclaration);

            // Assert
            _liteDbRepository.Received().Insert(testDeclaration);
            _liteDbRepository.Received().SingleById<Declaration>(testDeclaration.DeclarationId);
        }

        [Test]
        public void Throws_Declaration_Peristence_Error()
        {
            // Arrange
            _liteDbRepository.SingleById<Declaration>(testDeclaration.DeclarationId).Returns((Declaration)null);

            // Act/Assert
            Assert.Throws<DatabaseException>(() => _repositoryUnderTest.SaveDeclaration(testDeclaration), $"Failed to persist declatation with Id {testDeclaration.DeclarationId}");

            _liteDbRepository.Received().Insert(testDeclaration);
            _liteDbRepository.Received().SingleById<Declaration>(testDeclaration.DeclarationId);
        }

        [Test]
        public void Declatation_Retrieval_Success()
        {
            // Arrange
            var expectedExpression = (Expression<Func<Declaration, bool>>)(x => x.DeclarationId == testDeclaration.DeclarationId);
            _liteDbRepository.Single<Declaration>(Arg.Is<Expression<Func<Declaration, bool>>>(expr =>
                    Lambda.Eq(expr, expectedExpression)),null).Returns(testDeclaration);

            // Act
            var declaration = _repositoryUnderTest.GetDeclarationById(testDeclaration.DeclarationId);

            // Assert            
            Assert.AreEqual(testDeclaration, declaration);
            _liteDbRepository.Received().Single<Declaration>(Arg.Is<Expression<Func<Declaration, bool>>>(expr =>Lambda.Eq(expr, expectedExpression)), null);

        }
    }
}