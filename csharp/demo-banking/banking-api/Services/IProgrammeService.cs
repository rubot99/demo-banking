using banking_api.Endpoints.DataTransferObjects;
using banking_api.Models;

namespace banking_api.Services
{
    public interface IProgrammeService
    {
        public Task<ProgrammeDto> CreateAsync(CreateProgrammeDto command);

        public Task<ProgrammeDto?> GetByIdAsync(Guid programmeId);

        public Task<IEnumerable<ProgrammeDto>> GetAllAsync(Guid organisationId);

        public Task<bool> UpdateAsync(Programme programme);

        public Task<bool> DeleteAsync(Guid programmeId);
    }
}
