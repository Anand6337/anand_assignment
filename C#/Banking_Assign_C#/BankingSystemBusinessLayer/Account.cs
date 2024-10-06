using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class Account
    {
        internal int accountNumber;
        protected string accountType;
        internal double accountBalance;
        private static long accountNumberSeed  = 1001;
        private Customer customer;

        public int AccountBalance { get; internal set; }
        public long AccountNumber { get; internal set; }

        // Default Constructor
        public Account(int accountNumber, string v, double accountBalance) { }
        // Parameterized Constructor
        public Account(Customer customer, string accountType, float initialBalance)
        {
            this.accountNumber = (int)accountNumberSeed++;
            this.customer = customer;
            this.accountType = accountType;
            this.accountBalance = initialBalance;
        }

        // Method to print account details
        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}\nAccount Type: {accountType}\nAccount Balance: {accountBalance:C}");
        }

        // Overloaded Deposit Methods
        public void Deposit(float amount)
        {
            accountBalance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {accountBalance:C}");
        }

        public void Deposit(int amount)
        {
            accountBalance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {accountBalance:C}");
        }

        public void Deposit(double amount)
        {
            accountBalance += amount;
            Console.WriteLine($"Deposited {amount:C}. New balance: {accountBalance:C}");
        }

        // Overloaded Withdraw Methods
        public virtual void Withdraw(float amount)
        {
            if (amount <= accountBalance)
            {
                accountBalance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {accountBalance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public virtual void Withdraw(int amount)
        {
            if (amount <= accountBalance)
            {
                accountBalance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {accountBalance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= accountBalance)
            {
                accountBalance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. New balance: {accountBalance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        // Interest Calculation (Overridden in SavingsAccount)
        public virtual void CalculateInterest()
        {
            Console.WriteLine("Interest is not applicable for this account.");
        }
    }
}
