using LiteDB;
using System;

namespace JG.FinTech.GiftAid.Data.Entities
{
    /// <summary>
    /// Simple POCO to represent a declaration
    /// </summary>
    public class Declaration
    {
        /// <summary>
        /// Unique Identifier of the Declaration
        /// Only settable within the Data project
        /// </summary>        
        public Guid DeclarationId { get; internal set; }

        /// <summary>
        /// Name of the donot
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Post Code of the donor
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Donation amount
        /// </summary>
        public int DonationAmount { get; set; }
    }
}
