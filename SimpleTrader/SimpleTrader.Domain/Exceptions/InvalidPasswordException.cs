using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    internal class InvalidPasswordException : Exception
    {
        public string Password { get; set; }
        public string Username{ get; set; }
        public InvalidPasswordException(string password, string username)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string password, string username, string message) : base(message)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string password, string username, string message, Exception innerException) : base(message, innerException)
        {
            Password = password;
            Username = username;
        }
    }
}
