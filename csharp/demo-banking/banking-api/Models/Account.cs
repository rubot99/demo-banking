namespace banking_api.Models
{
    public class Account
    {
        public Guid Id { get;  set; }

        public string AccountNumber { get; set; }

        public string AccountHolderName { get; set; }

        public Account()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
