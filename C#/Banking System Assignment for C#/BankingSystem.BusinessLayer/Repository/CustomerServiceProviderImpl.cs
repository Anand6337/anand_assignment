using BankingSystem.BusinessLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Repository
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {

        private List<Account> accounts = new List<Account>();
        private List<Transaction> transactions = new List<Transaction>();

        public float GetAccountBalance(long accountNumber)
        {
            var account = accounts.Find(a => a.accountNumber == accountNumber);
            if (account == null) throw new InvalidOperationException("Account not found.");
            return account.accountBalance;
        }

        public float Deposit(long accountNumber, float amount)
        {
            var account = accounts.Find(a => a.accountNumber == accountNumber);
            if (account == null) throw new InvalidOperationException("Account not found.");
            account.accountBalance += amount;
            transactions.Add(new Transaction(account, "Deposit", "Deposit", amount));
            return account.accountBalance;
        }

        public float Withdraw(long accountNumber, float amount)
        {
            var account = accounts.Find(a => a.accountNumber == accountNumber);
            if (account == null) throw new InvalidOperationException("Account not found.");

            if (account is SavingsAccount && (account.accountBalance - amount < 500))
            {
                throw new InvalidOperationException("Insufficient funds; must maintain a minimum balance.");
            }

            if (account is CurrentAccount currentAccount && (account.accountBalance - amount < 0 && (amount - account.accountBalance) > currentAccount.OverdraftLimit))
            {
                throw new InvalidOperationException("Overdraft limit exceeded.");
            }

            account.accountBalance -= amount;
            transactions.Add(new Transaction(account, "Withdraw", "Withdraw", amount));
            return account.accountBalance;
        }

        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            var fromAccount = accounts.Find(a => a.accountNumber == fromAccountNumber);
            var toAccount = accounts.Find(a => a.accountNumber == toAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                throw new InvalidOperationException("Invalid account numbers.");
            }

            Withdraw(fromAccountNumber, amount);
            Deposit(toAccountNumber, amount);
        }

        public Account GetAccountDetails(long accountNumber)
        {
            var account = accounts.Find(a => a.accountNumber == accountNumber);
            if (account == null) throw new InvalidOperationException("Account not found.");
            return account;
        }

        public List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            
            return transactions.FindAll(t => t.account.accountNumber == accountNumber && t.transactionDateTime >= fromDate && t.transactionDateTime <= toDate);
        }
      
    }
}
