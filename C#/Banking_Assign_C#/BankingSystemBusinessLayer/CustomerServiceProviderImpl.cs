using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BusinessLayer;
using BankingSystemBusinessLayer.Service;

namespace BankingSystemBusinessLayer
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        protected List<Account> accountList = new List<Account>();

        public float GetAccountBalance(long accountNumber)
        {
            Account account = FindAccount(accountNumber);
            return account != null ? account.AccountBalance : 0;
        }

        public float Deposit(long accountNumber, float amount)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposited {amount}. New Balance: {account.AccountBalance}");
                return account.AccountBalance;
            }
            Console.WriteLine("Account not found.");
            return 0;
        }

        public float Withdraw(long accountNumber, float amount)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount);
                return account.AccountBalance;
            }
            Console.WriteLine("Account not found.");
            return 0;
        }

        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            Account fromAccount = FindAccount(fromAccountNumber);
            Account toAccount = FindAccount(toAccountNumber);

            if (fromAccount != null && toAccount != null && fromAccount.AccountBalance >= amount)
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
                Console.WriteLine($"Transferred {amount} from Account {fromAccountNumber} to {toAccountNumber}");
            }
            else
                Console.WriteLine("Transfer failed due to insufficient funds or invalid accounts.");
        }

        public void GetAccountDetails(long accountNumber)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
                account.PrintAccountInfo();
            else
                Console.WriteLine("Account not found.");
        }

        // Helper Method to find account by account number
        protected Account FindAccount(long accountNumber)
        {
            return accountList.Find(acc => acc.AccountNumber == accountNumber);
        }
    }
}
