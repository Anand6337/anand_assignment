using BankingSystem.BusinessLayer;
using BankingSystem.BusinessLayer.Repository;
using BankingSystem.BusinessLayer.Service;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BankingSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBankServiceProvider bankServiceProvider = new BankServiceProviderImpl();

            while (true)
            {
                Console.Clear(); // Clear the console for a cleaner UI experience
                Console.WriteLine("=====================================");
                Console.WriteLine("          BANKING SYSTEM MENU        ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("7. List All Accounts");
                Console.WriteLine("8. Get Transactions");
                Console.WriteLine("9. Exit");
                Console.WriteLine("=====================================");
                Console.Write("Enter your choice (1-9): ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 1 || choice > 9)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 9.");
                    WaitForUser();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateAccount(bankServiceProvider);
                        break;
                    case 2:
                        Deposit(bankServiceProvider);
                        break;
                    case 3:
                        Withdraw(bankServiceProvider);
                        break;
                    case 4:
                        GetBalance(bankServiceProvider);
                        break;
                    case 5:
                        Transfer(bankServiceProvider);
                        break;
                    case 6:
                        GetAccountDetails(bankServiceProvider);
                        break;
                    case 7:
                        ListAccounts(bankServiceProvider);
                        break;
                    case 8:
                        GetTransactions(bankServiceProvider);
                        break;
                    case 9:
                        Console.WriteLine("Exiting... Thank you for using the banking system.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                WaitForUser();
            }
        }
        private static void WaitForUser()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
        public static void CalculateInterest()
        {
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

                // Display query result
                Console.WriteLine("Employee ID\tEmployee Name\tEmployee Address\tEmployee Salary\tEmployee Manager");
                while (sqlDataReader.Read())
                {
                    Console.WriteLine($"{sqlDataReader["account_id"]}\t\t{sqlDataReader[1]}\t\t{sqlDataReader[2]}\t\t{sqlDataReader[3]}");
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
            Console.ReadKey();
        }

            private static void CreateAccount(IBankServiceProvider bankServiceProvider)
        {
            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email Address: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Account Type (Savings, Current, ZeroBalance): ");
            string accType = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            float balance = float.Parse(Console.ReadLine());

            Customer customer = new Customer(customerId, firstName, lastName, email, phoneNumber, address);

            try
            {
                //bankServiceProvider.CreateAccount(customer, accType, balance);
                bankServiceProvider.create_account(customer, 0, accType, balance); // Assuming 0 will be replaced with a generated account number
                Console.WriteLine("Account created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
            }
        }

        private static void Deposit(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("DEPOSIT FUNDS");
            Console.Write("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());

            Console.Write("Enter Deposit Amount: ");
            float amount = float.Parse(Console.ReadLine());

            try
            {
                float newBalance = bankServiceProvider.deposit(accountNumber, amount);
                Console.WriteLine($"Deposit successful. New balance: {newBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deposit: {ex.Message}");
            }
        }

        private static void Withdraw(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("WITHDRAW FUNDS");
            Console.Write("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());

            Console.Write("Enter Withdrawal Amount: ");
            float amount = float.Parse(Console.ReadLine());

            try
            {
                float newBalance = bankServiceProvider.withdraw(accountNumber, amount);
                Console.WriteLine($"Withdrawal successful. New balance: {newBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during withdrawal: {ex.Message}");
            }
        }

        private static void GetBalance(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("GET ACCOUNT BALANCE");
            Console.Write("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());

            try
            {
                float balance = bankServiceProvider.get_account_balance(accountNumber);
                Console.WriteLine($"Current balance: {balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving balance: {ex.Message}");
            }
        }

        private static void Transfer(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("TRANSFER FUNDS");
            Console.Write("Enter Sender's Account Number: ");
            long fromAccountNumber = long.Parse(Console.ReadLine());

            Console.Write("Enter Receiver's Account Number: ");
            long toAccountNumber = long.Parse(Console.ReadLine());

            Console.Write("Enter Transfer Amount: ");
            float amount = float.Parse(Console.ReadLine());

            try
            {
                bankServiceProvider.transfer(fromAccountNumber, toAccountNumber, amount);
                Console.WriteLine("Transfer successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during transfer: {ex.Message}");
            }
        }
        private static void GetAccountDetails(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("GET ACCOUNT DETAILS");
            Console.Write("Enter Account Number: ");
            if (!long.TryParse(Console.ReadLine(), out long accountNumber))
            {
                Console.WriteLine("Invalid account number format.");
                return;
            }

            try
            {
                Account accountDetails = bankServiceProvider.GetAccountDetails(accountNumber);
                Console.WriteLine(accountDetails); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        private static void ListAccounts(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("LIST OF ALL ACCOUNTS");
            try
            {
                var accounts = bankServiceProvider.ListAccounts();
                foreach (var account in accounts)
                {
                    Console.WriteLine(account); // Implement toString in Account class to display account details
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving account list: {ex.Message}");
            }
        }

        private static void GetTransactions(IBankServiceProvider bankServiceProvider)
        {
            Console.Clear();
            Console.WriteLine("GET TRANSACTIONS");
            Console.Write("Enter Account Number: ");
            long accountNumber = long.Parse(Console.ReadLine());

            Console.Write("Enter From Date (yyyy-mm-dd): ");
            DateTime fromDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter To Date (yyyy-mm-dd): ");
            //DateTime toDate = DateTime.Parse(Console.ReadLine());
            string input = Console.ReadLine();
            DateTime toDate;

            if (DateTime.TryParse(input, out toDate))
            {
                Console.WriteLine($"You entered a valid date: {toDate}");
            }
            else
            {
                Console.WriteLine("Invalid date format. Please try again.");
            }

            try
            {
                var transactions = bankServiceProvider.getTransations(accountNumber, fromDate, toDate);
                Console.WriteLine("Transaction History:");
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction); // Implement toString in Transaction class to display transaction details
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transactions: {ex.Message}");
            }


            //    //task 1 Conditional Statements 
            //    Console.WriteLine("\n----------TASK 1 conditional statements ----------");
            //    // Take customer input for credit score and annual income
            //    Console.WriteLine("Enter your credit score: ");
            //    int creditScore = int.Parse(Console.ReadLine());

            //    Console.WriteLine("Enter your annual income: ");
            //    decimal annualIncome = decimal.Parse(Console.ReadLine());

            //    // Determine loan eligibility
            //    bool isEligible = CheckLoanEligibility(creditScore, annualIncome);

            //    // Display the eligibility message
            //    if (isEligible)
            //    {
            //        Console.WriteLine("Congratulations! You are eligible for a loan.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Sorry, you are not eligible for a loan.");
            //    }

            //    //task 2 nested conditional statement
            //    Console.WriteLine("\n----------TASK 2 NESTED CONDITIONAL STATEMENT----------");
            //    Console.WriteLine("Enter your current balance: ");
            //    decimal currentBalance = decimal.Parse(Console.ReadLine());

            //    bool isRunning = true;

            //    while (isRunning)
            //    {
            //        // Display options for the user
            //        Console.WriteLine("\nATM Menu:");
            //        Console.WriteLine("1. Check Balance");
            //        Console.WriteLine("2. Withdraw");
            //        Console.WriteLine("3. Deposit");
            //        Console.WriteLine("4. Exit");
            //        Console.Write("Select an option (1-4): ");

            //        string option = Console.ReadLine();

            //        switch (option)
            //        {
            //            case "1":
            //                // Check Balance
            //                Console.WriteLine($"Your current balance is: {currentBalance:C}");
            //                break;

            //            case "2":
            //                // Withdraw money
            //                Console.WriteLine("Enter amount to withdraw (in multiples of 100 or 500): ");
            //                decimal withdrawAmount = decimal.Parse(Console.ReadLine());

            //                if (withdrawAmount <= currentBalance)
            //                {
            //                    if (withdrawAmount % 100 == 0 || withdrawAmount % 500 == 0)
            //                    {
            //                        currentBalance -= withdrawAmount;
            //                        Console.WriteLine($"Successfully withdrawn {withdrawAmount:C}. Your new balance is {currentBalance:C}");
            //                    }
            //                    else
            //                    {
            //                        Console.WriteLine("Withdrawal amount must be in multiples of 100 or 500.");
            //                    }
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Insufficient balance.");
            //                }
            //                break;

            //            case "3":
            //                // Deposit money
            //                Console.WriteLine("Enter amount to deposit: ");
            //                decimal depositAmount = decimal.Parse(Console.ReadLine());

            //                if (depositAmount > 0)
            //                {
            //                    currentBalance += depositAmount;
            //                    Console.WriteLine($"Successfully deposited {depositAmount:C}. Your new balance is {currentBalance:C}");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Deposit amount must be positive.");
            //                }
            //                break;

            //            case "4":
            //                // Exit the program
            //                Console.WriteLine("Thank you for using the ATM. Goodbye!");
            //                isRunning = false;
            //                break;

            //            default:
            //                Console.WriteLine("Invalid option. Please select a valid option (1-4).");
            //                break;
            //        }
            //    }


            //    //task 3 Looping
            //    Console.WriteLine("\n----------TASK 3 LOOPING----------");

            //    Console.WriteLine("Enter the number of customers: ");
            //    int numCustomers = int.Parse(Console.ReadLine());

            //    // Loop through each customer
            //    for (int i = 1; i <= numCustomers; i++)
            //    {
            //        Console.WriteLine($"\nCustomer {i}:");

            //        // Prompt for initial balance
            //        Console.WriteLine("Enter the initial balance: ");
            //        decimal initialBalance = decimal.Parse(Console.ReadLine());

            //        // Prompt for annual interest rate
            //        Console.WriteLine("Enter the annual interest rate (in %): ");
            //        decimal annualInterestRate = decimal.Parse(Console.ReadLine());

            //        // Prompt for number of years
            //        Console.WriteLine("Enter the number of years: ");
            //        int years = int.Parse(Console.ReadLine());

            //        // Calculate the future balance
            //        decimal futureBalance = CalculateFutureBalance(initialBalance, annualInterestRate, years);

            //        // Display the future balance
            //        Console.WriteLine($"The future balance for Customer {i} after {years} years will be: {futureBalance:C}");
            //    }
            //    //task 4 Looping, Array and Data Validation 
            //    Console.WriteLine("\n----------TASK 4 Looping, Array and Data Validation ----------");
            //    Dictionary<int, decimal> accounts = new Dictionary<int, decimal>
            //    {
            //        { 101, 1500.50m },
            //        { 102, 3200.00m },
            //        { 103, 5800.75m },
            //        { 104, 450.00m },
            //        { 105, 9876.99m }
            //    };

            //    bool is_Running = true;

            //    while (is_Running)
            //    {
            //        // Ask the user to input their account number
            //        Console.WriteLine("Please enter your account number (enter -1 to exit): ");
            //        string input = Console.ReadLine();

            //        // Check if the user wants to exit
            //        if (input == "-1")
            //        {
            //            Console.WriteLine("Exiting the system. Goodbye!");
            //            is_Running = false;
            //            break;
            //        }

            //        // Validate that the input is a valid integer account number
            //        if (int.TryParse(input, out int accountNumber))
            //        {
            //            // Check if the account number exists in the system
            //            if (accounts.ContainsKey(accountNumber))
            //            {
            //                // Display the account balance
            //                decimal balance = accounts[accountNumber];
            //                Console.WriteLine($"Account Number: {accountNumber}, Balance: {balance:C}");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Invalid account number. Please try again.");
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Invalid input. Please enter a valid account number.");
            //        }
            //    }
            //    //task 5 Password Validation
            //    Console.WriteLine("\n----------TASK 5 Password Validation----------");
            //    Console.WriteLine("Please create a password for your bank account:");
            //    string password = Console.ReadLine();

            //    // Validate the password
            //    bool isValid = ValidatePassword(password);

            //    if (isValid)
            //    {
            //        Console.WriteLine("Password created successfully.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Password creation failed. Please ensure your password meets the criteria.");
            //    }
            //    //task 6 Password Validation 
            //    Console.WriteLine("\n----------TASK 6 password validation ----------");
            //    // List to store transactions
            //    List<string> transactions = new List<string>();
            //    bool is__Running = true;

            //    // Loop to allow adding transactions
            //    while (is__Running)
            //    {
            //        // Display transaction menu
            //        Console.WriteLine("Choose a transaction type: ");
            //        Console.WriteLine("1. Deposit");
            //        Console.WriteLine("2. Withdraw");
            //        Console.WriteLine("3. Exit and Show Transaction History");
            //        string choice = Console.ReadLine();

            //        switch (choice)
            //        {
            //            case "1":
            //                // Deposit transaction
            //                Console.WriteLine("Enter deposit amount: ");
            //                decimal depositAmount = decimal.Parse(Console.ReadLine());
            //                transactions.Add($"Deposit: {depositAmount:C}");
            //                Console.WriteLine($"Successfully deposited {depositAmount:C}\n");
            //                break;

            //            case "2":
            //                // Withdraw transaction
            //                Console.WriteLine("Enter withdrawal amount: ");
            //                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
            //                transactions.Add($"Withdrawal: {withdrawAmount:C}");
            //                Console.WriteLine($"Successfully withdrew {withdrawAmount:C}\n");
            //                break;

            //            case "3":
            //                // Exit and show transaction history
            //                is__Running = false;
            //                break;

            //            default:
            //                Console.WriteLine("Invalid option. Please try again.");
            //                break;
            //        }
            //    }

            //    // Display the transaction history
            //    Console.WriteLine("\nTransaction History:");
            //    if (transactions.Count > 0)
            //    {
            //        foreach (var transaction in transactions)
            //        {
            //            Console.WriteLine(transaction);
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No transactions recorded.");
            //    }


            //    Console.ReadKey();
            //}

            //// Method to validate the password based on the rules
            //public static bool ValidatePassword(string password)
            //{
            //    // Check if password is at least 8 characters long
            //    if (password.Length < 8)
            //    {
            //        Console.WriteLine("Password must be at least 8 characters long.");
            //        return false;
            //    }

            //    // Check if password contains at least one uppercase letter
            //    bool hasUpperCase = false;
            //    bool hasDigit = false;

            //    foreach (char c in password)
            //    {
            //        if (char.IsUpper(c))
            //        {
            //            hasUpperCase = true;
            //        }

            //        // Check if password contains at least one digit
            //        if (char.IsDigit(c))
            //        {
            //            hasDigit = true;
            //        }

            //        // If both conditions are met, stop the loop
            //        if (hasUpperCase && hasDigit)
            //        {
            //            break;
            //        }
            //    }

            //    // Check if uppercase letter exists
            //    if (!hasUpperCase)
            //    {
            //        Console.WriteLine("Password must contain at least one uppercase letter.");
            //        return false;
            //    }

            //    // Check if at least one digit exists
            //    if (!hasDigit)
            //    {
            //        Console.WriteLine("Password must contain at least one digit.");
            //        return false;
            //    }

            //    // If all conditions are met, return true
            //    return true;
            //}

            //// Method to calculate future balance using the compound interest formula
            //public static decimal CalculateFutureBalance(decimal initialBalance, decimal annualInterestRate, int years)
            //{
            //    // Formula: future_balance = initial_balance * (1 + annual_interest_rate/100)^years
            //    decimal futureBalance = initialBalance * (decimal)Math.Pow((double)(1 + annualInterestRate / 100), years);
            //    return futureBalance;
            //}

            //// Method to check loan eligibility based on credit score and annual income
            //public static bool CheckLoanEligibility(int creditScore, decimal annualIncome)
            //{
            //    // Eligibility criteria: Credit score >= 650 and Annual income >= 30,000
            //    if (creditScore >= 650 && annualIncome >= 30000)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        // Display specific reasons why the customer is ineligible
            //        if (creditScore < 650)
            //        {
            //            Console.WriteLine("Your credit score is too low. Minimum required is 650.");
            //        }

            //        if (annualIncome < 30000)
            //        {
            //            Console.WriteLine("Your annual income is too low. Minimum required is $30,000.");
            //        }

            //        return false;
            //    }


            //}

        }
}
}
