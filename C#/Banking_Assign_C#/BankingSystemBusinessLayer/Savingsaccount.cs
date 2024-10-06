using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class SavingsAccount : Account
    {
        private double interestRate;
        private Customer customer;
        private float initialBalance;

        public SavingsAccount(Customer customer, float initialBalance)
        {
            this.customer = customer;
            this.initialBalance = initialBalance;
        }

        public SavingsAccount(int accountNumber, double accountBalance, double interestRate)
            : base(accountNumber, "Savings", accountBalance)
        {
            this.interestRate = interestRate;
        }

        // Override Interest Calculation Method
        public override void CalculateInterest()
        {
            double interest = (accountBalance * interestRate) / 100;
            accountBalance += interest;
            Console.WriteLine($"Interest of {interestRate}% applied. New balance: {accountBalance:C}");
        }
    }
}
