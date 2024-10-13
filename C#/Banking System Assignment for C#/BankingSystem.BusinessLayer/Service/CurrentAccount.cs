using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    public class CurrentAccount : BankAccount
    {
        private const double overdraftLimit = 1000.00;

        // Constructor
        public CurrentAccount(int accountNumber, string customerName, double balance)
            : base(accountNumber, customerName, balance) { }

        // Implement Deposit method
        public override void Deposit(float amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {balance:C}");
        }

        // Override Withdraw method to allow overdraft
        public override void Withdraw(float amount)
        {
            if (amount <= balance + overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {balance:C}");
            }
            else
            {
                Console.WriteLine($"Withdrawal exceeds overdraft limit of {overdraftLimit:C}.");
            }
        }

        // Override CalculateInterest method (no interest for CurrentAccount)
        public override void CalculateInterest()
        {
            Console.WriteLine("No interest for Current Account.");
        }
    }
}
