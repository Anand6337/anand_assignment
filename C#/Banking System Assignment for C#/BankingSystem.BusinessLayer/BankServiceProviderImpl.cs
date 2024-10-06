using BankingSystem.BusinessLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    // Bank Service Implementation
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        private string branchName;
        private string branchAddress;

        public BankServiceProviderImpl(string branchName, string branchAddress)
        {
            this.branchName = branchName;
            this.branchAddress = branchAddress;
        }

        public void CreateAccount(Customer customer, string accountType, float initialBalance)
        {
            Account newAccount = null;

            switch (accountType.ToLower())
            {
                case "savings":
                    newAccount = new SavingsAccount(customer, initialBalance);
                    break;
                case "current":
                    newAccount = new CurrentAccount(customer, initialBalance);
                    break;
                case "zerobalance":
                    newAccount = new ZeroBalanceAccount(customer);
                    break;
                default:
                    Console.WriteLine("Invalid account type.");
                    return;
            }

            accountList.Add(newAccount);
            Console.WriteLine("Account created successfully!");
            newAccount.PrintAccountInfo();
        }

        public Account[] ListAccounts()
        {
            return accountList.ToArray();
        }

        public void CalculateInterest()
        {
            foreach (var account in accountList)
            {
                float interest = (float)account.CalculateInterest();
                if (interest > 0)
                {
                    account.Deposit(interest);
                    Console.WriteLine($"Interest {interest} added to Account {account.AccountNumber}. New Balance: {account.AccountBalance}");
                }
            }
        }
    }
}
