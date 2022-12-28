using System;


namespace SimpleTrader.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public string Password { get; set; }
        public string Username{ get; set; }
        public InvalidPasswordException(string username, string password)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string username, string password, string message) : base(message)
        {
            Password = password;
            Username = username;
        }

        public InvalidPasswordException(string username, string password, string message, Exception innerException) : base(message, innerException)
        {
            Password = password;
            Username = username;
        }
    }
}
