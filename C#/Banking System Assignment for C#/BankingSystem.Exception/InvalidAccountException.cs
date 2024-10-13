using System;

namespace BankingSystem.Exceptions
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException() : base("Invalid account number provided.")
        {
        }

        public InvalidAccountException(string message) : base(message)
        {
        }
    }
}
