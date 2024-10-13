using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    //IBankServiceProvider Interface
    public interface IBankServiceProvider
    {
        
        Account GetAccountDetails(long accountNumber);
        void create_account(Customer customer, int v, string accType, float balance);
        float deposit(long accountNumber, float amount);
        float withdraw(long accountNumber, float amount);
        float get_account_balance(long accountNumber);
        void transfer(long fromAccountNumber, long toAccountNumber, float amount);
        IEnumerable<Account> ListAccounts();
        IEnumerable<object> getTransations(long accountNumber, DateTime fromDate, DateTime toDate);
        
    }
}
