using System;

namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidResponseException : Exception
    {

        public string ResponseUri { get; set; }
        public InvalidResponseException(string responseUri)
        {
            ResponseUri = responseUri;
        }

        public InvalidResponseException(string responseUri, string message) : base(responseUri)
        {
        }

        public InvalidResponseException(string responseUri, Exception innerException) : base(responseUri, innerException)
        {
        }
    }
}
