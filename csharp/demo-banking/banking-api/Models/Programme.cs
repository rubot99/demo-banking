namespace banking_api.Models
{
    public class Programme : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public Organisation Organisation { get; set; } = null!;
        public Guid OrganisationId { get; set; }

        public Programme(string name, string description, DateTimeOffset startDate, DateTimeOffset endDate, int startRange, int endRange, Guid organisationId)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            StartRange = startRange;
            EndRange = endRange;
            OrganisationId = organisationId;
        }

        public void Update(string name, string description, DateTimeOffset startDate, DateTimeOffset endDate, int startRange, int endRange)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            StartRange = startRange;
            EndRange = endRange;
            UpdateTimestamp();
        }

        public static Programme Create(string name, string description, DateTimeOffset startDate, DateTimeOffset endDate, int startRange, int endRange, Guid organisationId)
        {
            return new Programme(name, description, startDate, endDate, startRange, endRange, organisationId);
        }
    }
}
