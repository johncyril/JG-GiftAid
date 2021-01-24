using JG.FinTech.GiftAid.Data.Entities;
using LiteDB;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JG.FinTech.GiftAid.Data.Unit.Tests")]
namespace JG.FinTech.GiftAid.Data
{
    /// <summary>
    /// A simple repository class for crud operations on stored documents.
    /// This could certainly be genericised
    /// </summary>    
    public class Repository : IRepository
    {
        private readonly ILiteRepository _liteRepository;

        public Repository(ILiteRepository liteRepository)
        {
            _liteRepository = liteRepository;            
        }

        public Declaration GetDeclarationById(Guid declarationId)
        {
            return _liteRepository.Single<Declaration>(x => x.DeclarationId == declarationId);
        }

        public void SaveDeclaration(Declaration declaration)
        {
            if (declaration.DeclarationId == default(Guid))
            {
                declaration.DeclarationId = Guid.NewGuid();
            }

            _liteRepository.Insert(declaration);
            if (_liteRepository.SingleById<Declaration>(declaration.DeclarationId) == null)
            {
                throw new DatabaseException($"Failed to persist declatation with Id {declaration.DeclarationId}");
            }
        }
    }
}
