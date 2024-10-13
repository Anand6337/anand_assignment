using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    // ICustomerServiceProvider Interface
    public interface ICustomerServiceProvider
    {
        float GetAccountBalance(long accountNumber);
        float Deposit(long accountNumber, float amount);
        float Withdraw(long accountNumber, float amount);
        void Transfer(long fromAccountNumber, long toAccountNumber, float amount);
        Account GetAccountDetails(long accountNumber);
        List<Transaction> GetTransactions(long accountNumber, DateTime fromDate, DateTime toDate);
    }
}
