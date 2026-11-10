namespace banking_api.Endpoints.DataTransferObjects
{
    public record ProgrammeDto(Guid Id, string Name, string Description, DateTimeOffset StartDate, DateTimeOffset EndDate, int StartRange, int EndRange, Guid OrganisationId);
}
