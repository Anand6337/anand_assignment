using BankingSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Repository
{
    public class BankRepositoryImpl : IBankRepository
    {
        private readonly string connectionString = "YourConnectionStringHere";

        public void CreateAccount(Customer customer, long accNo, string accType, float balance)
        {
            using (var conn = DBUtil.GetDBConn())
            {

            }
        }

        public float Deposit(long accountNumber, float amount)
        {
            throw new NotImplementedException();
        }

        public float GetAccountBalance(long accountNumber)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountDetails(long accountNumber)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public List<Account> ListAccounts()
        {
            throw new NotImplementedException();
        }

        public void Transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            throw new NotImplementedException();
        }

        public float Withdraw(long accountNumber, float amount)
        {
            throw new NotImplementedException();
        }

    }
}
