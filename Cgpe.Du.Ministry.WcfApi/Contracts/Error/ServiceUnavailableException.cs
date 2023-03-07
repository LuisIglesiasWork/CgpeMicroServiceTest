using System;
using System.Runtime.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    public class ServiceUnavailableException : Exception
    {

        public ServiceUnavailableException() : base() { }

        public ServiceUnavailableException(string message) : base(message) { }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }

        public ServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)  { }

    }

}