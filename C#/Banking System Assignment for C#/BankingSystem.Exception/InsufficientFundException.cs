using System;

namespace BankingSystem.Exceptions
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException() : base("Insufficient funds in the account.")
        {
        }

        public InsufficientFundException(string message) : base(message)
        {
        }
    }
}
