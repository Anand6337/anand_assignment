using BankingSystem.BusinessLayer.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BankingSystem.Util;


namespace BankingSystem.BusinessLayer.Repository
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        private object accountList;

        public Account GetAccountDetails(long accountNumber)
        {
            Account account = null;

            var conn = DBUtil.GetDBConn();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT a.account_id, a.account_type, a.balance, 
                         c.customer_id, c.first_name, c.last_name, 
                         c.email, c.phone_number, c.address
                         FROM Accounts a
                         INNER JOIN Customers c ON a.customer_id = c.customer_id
                         WHERE a.account_id = @accountNumber";
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int accountId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("account_id"));
                    string accountType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("transaction_type"));
                    float balance = (float)sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("balance"));


                    int customerId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("customer_id"));
                    string firstName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("first_name"));
                    string lastName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("last_name"));
                    string email = sqlDataReader.GetString(sqlDataReader.GetOrdinal("email"));
                    string phone = sqlDataReader.GetString(sqlDataReader.GetOrdinal("phone_number"));
                    string address = sqlDataReader.GetString(sqlDataReader.GetOrdinal("address"));
                          
                    Customer customer = new Customer(customerId,firstName,lastName,email,phone,address);
                    switch (accountType)
                    {
                        case "savings":
                            account = new SavingsAccount(balance, customer) { accountNumber = accountNumber };
                            break;

                        case "current":
                            account = new CurrentAccount(balance, customer) { accountNumber = accountNumber };
                            break;

                        case "zerobalance":
                            account = new ZeroBalanceAccount(customer) { accountNumber = accountNumber };
                            break;
                        default:
                            throw new ArgumentException("Invalid account type.");
                    }
                }
                sqlDataReader.Close();

            }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"Database error: {sqlEx.Message}");
                }
                catch (InvalidCastException castEx)
                {
                    Console.WriteLine($"Data type mismatch: {castEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return account;
        }

        public void create_account(Customer customer, int v, string accType, float balance)
        {
            var conn = DBUtil.GetDBConn();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute the query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Accounts (account_type, balance, customer_id) VALUES (@AccountType, @Balance, @CustomerID)";
                cmd.Connection = conn;

                // Set parameters for the query
                cmd.Parameters.AddWithValue("@AccountType", accType);
                cmd.Parameters.AddWithValue("@Balance", balance);
                cmd.Parameters.AddWithValue("@CustomerID", customer.customerId);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    
        public float deposit(long accountNumber, float amount)
        {
            float newBalance = 0;
            var conn = DBUtil.GetDBConn();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute the query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Accounts SET balance = Balance + @Amount WHERE account_id = @AccountNumber; " +
                               "SELECT Balance FROM Accounts WHERE account_id = @AccountNumber";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

              

                // Execute the query and safely convert the result
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    newBalance = Convert.ToSingle(result);  // Safely convert to float
                }
                else
                {
                    throw new InvalidOperationException("Account not found or balance is null.");
                }
            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return newBalance;
        }

        public float withdraw(long accountNumber, float amount)
        {
            float newBalance = 0;
            var conn = DBUtil.GetDBConn();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute the query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE Accounts SET balance = balance - @Amount WHERE account_id = @AccountNumber AND balance >= @Amount; " +
                               "SELECT Balance FROM Accounts WHERE account_id = @AccountNumber";
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

                // Execute the query and safely convert the result
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    newBalance = Convert.ToSingle(result);  // Safely convert to float
                }
                else
                {
                    throw new InvalidOperationException("Account not found or balance is null.");
                }
            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return newBalance;
        }

        public float get_account_balance(long accountNumber)
        {
            float balance = 0;

            var conn = DBUtil.GetDBConn();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Balance FROM Accounts WHERE account_id = @accountNumber";
                cmd.Connection = conn;

                // Use parameter to safely pass the account number to the SQL query
                cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                // Execute the query and safely convert the result
                var result = cmd.ExecuteScalar();

                // Check if result is null and cast to the appropriate type
                if (result != null && result != DBNull.Value)
                {
                    balance = Convert.ToSingle(result);  // Convert the result to float
                }
                else
                {
                    Console.WriteLine("Account not found or balance is null.");
                }
            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return balance;
        }

        public void transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            var conn = DBUtil.GetDBConn();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }

                try
                {
                    SqlCommand withdrawCmd = new SqlCommand();
                    withdrawCmd.CommandText = "UPDATE Accounts SET balance = balance - @Amount WHERE account_id = @FromAccount AND balance >= @Amount";
                    withdrawCmd.Connection = conn;
                    withdrawCmd.Parameters.AddWithValue("@Amount", amount);
                    withdrawCmd.Parameters.AddWithValue("@FromAccount", fromAccountNumber);
                    withdrawCmd.ExecuteNonQuery();

                    SqlCommand depositCmd = new SqlCommand();
                    depositCmd.CommandText = "UPDATE Accounts SET balance = balance + @Amount WHERE account_id = @ToAccount";
                    depositCmd.Connection = conn;
                    depositCmd.Parameters.AddWithValue("@Amount", amount);
                    depositCmd.Parameters.AddWithValue("@ToAccount", toAccountNumber);
                depositCmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Transfer failed.");
                }
            
        }
        public IEnumerable<Account> ListAccounts()
        {
            List<Account> accounts = new List<Account>();
            var conn = DBUtil.GetDBConn();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Accounts";
                cmd.Connection = conn;

                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Account account;
                    int accountNumber = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("account_id"));
                    int customerNumber = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("customer_id"));
                    string accountType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("account_type"));
                    float balance = (float)sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("balance"));

                    Customer customer = new Customer(
                            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("customer_id")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("first_name")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("last_name")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("email")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("phone_number")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("address"))
                        );


                    switch (accountType.ToLower())
                    {
                        case "savings":
                            account = new SavingsAccount(balance, customer) { accountNumber = accountNumber };
                            break;

                        case "current":
                            account = new CurrentAccount(balance, customer) { accountNumber = accountNumber };
                            break;

                        case "zerobalance":
                            account = new ZeroBalanceAccount(customer) { accountNumber = accountNumber };
                            break;

                        default:
                            throw new ArgumentException("Invalid account type.");
                    }

                    accounts.Add(account);


                }
                // Close the reader after processing the result
                sqlDataReader.Close();

            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return accounts;
        }


        public IEnumerable<object> getTransations(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            List<object> transactions = new List<object>();
            var conn = DBUtil.GetDBConn();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open(); // Only open the connection if it's closed
            }
            try
            {
                // Prepare and execute query
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Transactions WHERE account_id = @AccountNumber AND transaction_date BETWEEN @FromDate AND @toDate";
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);

                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {        
                    //int transactionId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("transaction_id"));
                    //int accountId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("account_id"));
                    //string accountType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("transaction_type"));
                    //string otherValue = sqlDataReader.GetValue(3).ToString(); // Access by index
                transactions.Add(new
                {
                    transactionId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("transaction_id")),
                    accountId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("account_id")),
                    accountType = sqlDataReader.GetString(sqlDataReader.GetOrdinal("transaction_type")),
                    otherValue = sqlDataReader.GetValue(3).ToString(), // Access by index
                });
            }
                    // Close the reader after processing the result
                    sqlDataReader.Close();
            }
            finally
            {
                // Ensure that the connection is properly closed after execution
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
           return transactions;
        }
    }
    
}
