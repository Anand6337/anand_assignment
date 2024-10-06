using System;
using BankingSystem.BusinessLayer;

namespace BankingSystemBusinessLayer
{
    // Zero Balance Account
    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer customer)
            : base(customer, "ZeroBalance", 0) { }

        public override void Withdraw(float amount)
        {
            if (accountBalance - amount >= 0)
                accountBalance -= amount;
            else
                Console.WriteLine("Insufficient balance!");
        }

        public override void Deposit(float amount)
        {
            accountBalance += amount;
        }

        public override float CalculateInterest()
        {
            return 0; // No interest for zero balance account
        }
    }
}