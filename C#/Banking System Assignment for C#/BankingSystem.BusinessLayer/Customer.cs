namespace BankingSystem.BusinessLayer
{
    public class Customer
    {
        
        public long customerId;
        public string firstName;
        public string lastName;
        public string emailAddress;
        public string phoneNumber;
        public string address;

        public Customer() { }
        public Customer(long customerId, string firstName, string lastName, string email, string phoneNumber, string address)
        {
            this.customerId = customerId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}, {emailAddress}, {phoneNumber}, {address}";
        }
    }
}
