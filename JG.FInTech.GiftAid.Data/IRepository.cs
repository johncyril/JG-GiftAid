using JG.FinTech.GiftAid.Data.Entities;
using System;

namespace JG.FinTech.GiftAid.Data
{
    public interface IRepository
    {
        void SaveDeclaration(Declaration declaration);

        Declaration GetDeclarationById(Guid declarationId);
    }
}