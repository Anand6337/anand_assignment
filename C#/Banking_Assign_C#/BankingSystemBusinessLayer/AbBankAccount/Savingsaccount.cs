using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemBusinessLayer.AbBankAccount
{
    public class SavingsAccount : BankAccount
    {
        private double interestRate;

        // Constructor
        public SavingsAccount(int accountNumber, string customerName, double balance, double interestRate)
            : base(accountNumber, customerName, balance)
        {
            this.interestRate = interestRate;
        }

        // Implement Deposit method
        public override void Deposit(float amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {balance:C}");
        }

        // Implement Withdraw method
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

        // Implement CalculateInterest method
        public override void CalculateInterest()
        {
            double interest = (balance * interestRate) / 100;
            balance += interest;
            Console.WriteLine($"Interest of {interestRate}% applied. New balance: {balance:C}");
        }
    }
}
