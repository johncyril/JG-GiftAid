using System;
using System.Runtime.Serialization;

namespace JG.FinTech.GiftAid.Api.Exceptions
{
    [Serializable]
    public class GiftAidException : Exception
    {
        public GiftAidException()
        {
        }

        public GiftAidException(string message) : base(message)
        {
        }

        public GiftAidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GiftAidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}