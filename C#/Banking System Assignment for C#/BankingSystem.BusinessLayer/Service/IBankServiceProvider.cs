using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer.Service
{
    // IBankServiceProvider Interface
    public interface IBankServiceProvider : ICustomerServiceProvider
    {
        void CreateAccount(Customer customer, string accountType, float initialBalance);
        Account[] ListAccounts();
        void CalculateInterest();
    }
}
