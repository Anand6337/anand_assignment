using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BusinessLayer;

namespace BankingSystemBusinessLayer.Service
{
    // IBankServiceProvider Interface
    public interface IBankServiceProvider : ICustomerServiceProvider
    {
        void CreateAccount(Customer customer, string accountType, float initialBalance);
        Account[] ListAccounts();
        void CalculateInterest();
    }
}
