using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();

        // Method to create a new account
        public void CreateAccount(Customer customer, string accountType, float initialBalance)
        {
            Account newAccount = new Account(customer, accountType, initialBalance);
            accounts.Add(newAccount);
            Console.WriteLine("Account created successfully.");
            newAccount.PrintAccountInfo();
        }

        // Method to get account balance by account number
        public float GetAccountBalance(long accountNumber)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                return (float)account.accountBalance;
            }
            else
            {
                Console.WriteLine("Account not found.");
                return 0;
            }
        }

        // Method to deposit amount
        public float Deposit(long accountNumber, float amount)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.accountBalance += amount;
                Console.WriteLine($"Deposited {amount:C}. New balance: {account.accountBalance:C}");
                return (float) account.accountBalance;
            }
            else
            {
                Console.WriteLine("Account not found.");
                return 0;
            }
        }

        // Method to withdraw amount
        public float Withdraw(long accountNumber, float amount)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                if (amount <= account.accountBalance)
                {
                    account.accountBalance -= amount;
                    Console.WriteLine($"Withdrew {amount:C}. New balance: {account.accountBalance:C}");
                    return (float) account.accountBalance;
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                    return  (float)account.accountBalance;
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
                return 0;
            }
        }

        // Method to transfer funds between accounts
        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            Account fromAccount = FindAccount(fromAccountNumber);
            Account toAccount = FindAccount(toAccountNumber);

            if (fromAccount != null && toAccount != null)
            {
                if (fromAccount.accountBalance >= amount)
                {
                    fromAccount.accountBalance -= amount;
                    toAccount.accountBalance += amount;
                    Console.WriteLine($"Transferred {amount:C} from Account {fromAccountNumber} to Account {toAccountNumber}.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds for transfer.");
                }
            }
            else
            {
                Console.WriteLine("One or both accounts not found.");
            }
        }

        // Method to get account and customer details
        public void GetAccountDetails(long accountNumber)
        {
            Account account = FindAccount(accountNumber);
            if (account != null)
            {
                account.PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Helper method to find account by account number
        private Account FindAccount(long accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (account.accountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }
}
