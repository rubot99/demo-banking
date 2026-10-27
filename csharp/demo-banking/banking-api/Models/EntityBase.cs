namespace banking_api.Models
{
    public class EntityBase
    {
        public Guid Id { get; init; } = Guid.CreateVersion7();
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.UtcNow;

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
