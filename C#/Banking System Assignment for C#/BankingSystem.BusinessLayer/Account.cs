using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class Account
    {
        public static long lastAccNo = 1001;
        public Customer customer;
        public long accountNumber;
        public string accountType;
        public float accountBalance;

        public Account() { }
        public Account(string accountType, float initialBalance, Customer customer)
        {
            this.accountNumber = ++lastAccNo; 
            this.accountType = accountType;
            this.accountBalance = initialBalance;
            this.customer = customer;
        }
        public override string ToString()
        {
            return $"AccountNumber: {accountNumber}, Type: {accountType},Balance: {accountBalance} ";
        }
    }
}
