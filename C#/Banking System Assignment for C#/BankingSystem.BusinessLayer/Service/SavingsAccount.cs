using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    public class SavingsAccount : BankAccount
    {
        private double interestRate;

        public SavingsAccount(int accountNumber, string customerName, double balance, double interestRate)
            : base(accountNumber, customerName, balance)
        {
            this.interestRate = interestRate;
        }

        public override void Deposit(float amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {balance:C}");
        }

        public override void Withdraw(float amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public override void CalculateInterest()
        {
            double interest = (balance * interestRate) / 100;
            balance += interest;
            Console.WriteLine($"Interest of {interestRate}% applied. New balance: {balance:C}");
        }
    }
}
