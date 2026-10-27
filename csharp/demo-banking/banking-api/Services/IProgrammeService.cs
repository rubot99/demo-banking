using banking_api.Models;

namespace banking_api.Services
{
    public interface IProgrammeService
    {
        public Task<bool> CreateAsync(Programme programme);

        public Task<Programme?> GetByIdAsync(Guid programmeId);

        public Task<IEnumerable<Programme>> GetAllAsync(Guid organisationId);

        public Task<bool> UpdateAsync(Programme programme);

        public Task<bool> DeleteAsync(Guid programmeId);
    }
}
