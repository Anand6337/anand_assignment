using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    public abstract class BankAccount
    {
        protected int accountNumber;
        protected string customerName;
        protected double balance;

        public BankAccount() { }

        public BankAccount(int accountNumber, string customerName, double balance)
        {
            this.accountNumber = accountNumber;
            this.customerName = customerName;
            this.balance = balance;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}\nCustomer Name: {customerName}\nAccount Balance: {balance:C}");
        }
        public abstract void Deposit(float amount);
        public abstract void Withdraw(float amount);
        public abstract void CalculateInterest();
    }
}
