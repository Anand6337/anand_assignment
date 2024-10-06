using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public abstract class BankAccount
    {
        // Attributes
        protected int accountNumber;
        protected string customerName;
        protected double balance;

        // Default Constructor
        public BankAccount() { }

        // Parameterized Constructor
        public BankAccount(int accountNumber, string customerName, double balance)
        {
            this.accountNumber = accountNumber;
            this.customerName = customerName;
            this.balance = balance;
        }

        // Method to display account details
        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}\nCustomer Name: {customerName}\nAccount Balance: {balance:C}");
        }

        // Abstract methods
        public abstract void Deposit(float amount);
        public abstract void Withdraw(float amount);
        public abstract void CalculateInterest();
    }
}
