namespace banking_api.Models
{
    public class Programme : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public Guid OrganisationId { get; set; }

        public Programme()
        {
            Id = Guid.CreateVersion7();
        }
    }
}
