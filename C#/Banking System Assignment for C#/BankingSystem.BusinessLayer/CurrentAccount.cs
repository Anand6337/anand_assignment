using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BusinessLayer
{
    public class CurrentAccount : Account
    {
        public float OverdraftLimit { get; set; } = 1000; 
        public CurrentAccount(float initialBalance, Customer customer)
            : base("Current", initialBalance, customer) { }

        public override string ToString()
        {
            return $"CurrentAccount: {base.ToString()}, OverdraftLimit: {OverdraftLimit}";
        }
    }
}
