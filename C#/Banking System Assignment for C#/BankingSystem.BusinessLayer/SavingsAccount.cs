using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class SavingsAccount : Account
    {
        public float InterestRate { get; set; } = 0.03f; 
        private const float MinimumBalance = 500;

        public SavingsAccount(float initialBalance, Customer customer)
            : base("Savings", initialBalance, customer)
        {
            if (initialBalance < MinimumBalance)
            {
                throw new ArgumentException($"Initial balance must be at least {MinimumBalance}");
            }
        }
        public override string ToString()
        {
            return $"SavingsAccount: {base.ToString()}";
        }
    }
}
