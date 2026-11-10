namespace banking_api.Endpoints.DataTransferObjects
{
    public record CreateProgrammeDto(string Name, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, int StartRange, int EndRange, Guid OrganisationId);
}
