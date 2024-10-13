using System;

namespace BankingSystem.Exceptions
{
    public class OverDraftLimitExceededException : Exception
    {
        public OverDraftLimitExceededException() : base("Overdraft limit exceeded.")
        {
        }

        public OverDraftLimitExceededException(string message) : base(message)
        {
        }
    }
}