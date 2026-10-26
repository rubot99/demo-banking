namespace banking_api.Models
{
    public class Organisation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Organisation()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
