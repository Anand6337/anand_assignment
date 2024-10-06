using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class CurrentAccount : Account
    {
        private const double overdraftLimit = 1000.00;
        private Customer customer;
        private float initialBalance;

        public CurrentAccount(int accountNumber, double accountBalance)
            : base(accountNumber, "Current", accountBalance) { }

        public CurrentAccount(Customer customer, float initialBalance)
        {
            this.customer = customer;
            this.initialBalance = initialBalance;
        }

        // Override Withdraw Method to Allow Overdraft
        public override void Withdraw(double amount)
        {
            if (amount <= accountBalance + overdraftLimit)
            {
                accountBalance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {accountBalance:C}");
            }
            else
            {
                Console.WriteLine($"Withdrawal exceeds overdraft limit of {overdraftLimit:C}.");
            }
        }
    }
}
