using banking_api.Models;

namespace banking_api.Services
{
    public class ProgrammeService : IProgrammeService
    {
        public Task<bool> CreateAsync(Programme programme)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid programmeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Programme>> GetAllAsync(Guid organisationId)
        {
            throw new NotImplementedException();
        }

        public Task<Programme?> GetByIdAsync(Guid programmeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Programme programme)
        {
            throw new NotImplementedException();
        }
    }
}
