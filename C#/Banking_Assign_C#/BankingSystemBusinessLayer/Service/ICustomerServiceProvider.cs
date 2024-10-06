using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemBusinessLayer.Service
{
    // ICustomerServiceProvider Interface
    public interface ICustomerServiceProvider
    {
        float GetAccountBalance(long accountNumber);
        float Deposit(long accountNumber, float amount);
        float Withdraw(long accountNumber, float amount);
        void Transfer(long fromAccount, long toAccount, float amount);
        void GetAccountDetails(long accountNumber);
    }
}
