namespace banking_api.Models
{
    public class Organisation : EntityBase
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }

        public Organisation()
        {
            Name = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
        }

        public Organisation(string name, string address, string email)
        {
            Name = name;
            Address = address;
            Email = email;
        }

        public void Update(string name, string address, string email)
        {
            Name = name;
            Address = address;
            Email = email;
            UpdateTimestamp();
        }

        public static Organisation Create(string name, string address, string email)
        {
            return new Organisation(name, address, email);
        }
    }
}
