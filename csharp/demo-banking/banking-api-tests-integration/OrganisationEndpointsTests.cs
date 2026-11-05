using banking_api;
using banking_api.Endpoints.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace banking_api_tests_integration
{
    public class OrganisationEndpointsTests : IClassFixture<WebApplicationFactory<IApiMarker>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<IApiMarker> _factory;
        private readonly List<Guid> _createdOrganisationIds = new();

        public OrganisationEndpointsTests(WebApplicationFactory<IApiMarker> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateOrganisation_CreatesOrganisation_WhenDataIsValid()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            var organisation = new CreateOrganisationDto("Test Organisation", "123 Test Street", "test@organisation.com");

            // Act  
            var result = await httpClient.PostAsJsonAsync("/organisations", organisation);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdOrganisation = await result.Content.ReadFromJsonAsync<OrganisationDto>();
            _createdOrganisationIds.Add(createdOrganisation!.Id);
            createdOrganisation.Should().BeEquivalentTo(organisation);
            result.Headers.Location.Should().Be($"http://localhost/organisations/{createdOrganisation.Id}");
        }

        [Fact]
        public async Task CreateOrganisation_Fails_WhenEmailIsInvalid()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            var organisation = new CreateOrganisationDto("Test Organisation", "123 Test Street", "invalid-email");

            // Act
            var result = await httpClient.PostAsJsonAsync("/organisations", organisation);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationError>>();
            var error = errors!.Single();
            error.PropertyName.Should().Be("Email");
            error.ErrorMessage.Should().Be("Organisation email is not valid.");
        }

        [Fact]
        public async Task GetOrganisation_ReturnsOrganisation_WhenOrganisationExists()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            var organisation = new CreateOrganisationDto("Test Organisation", "123 Test Street", "test@organisation.com");
            var createdResult = await httpClient.PostAsJsonAsync("/organisations", organisation);
            var createdOrganisation = await createdResult.Content.ReadFromJsonAsync<OrganisationDto>();
            _createdOrganisationIds.Add(createdOrganisation!.Id);

            // Act
            var getResult = await httpClient.GetAsync($"/organisations/{createdOrganisation.Id}");

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var existingOrganisation = await getResult.Content.ReadFromJsonAsync<OrganisationDto>();
            existingOrganisation.Should().BeEquivalentTo(createdOrganisation);
        }

        [Fact]
        public async Task GetOrganisation_ReturnsNotFound_WhenOrganisationDoesNotExist()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            var organisation = new CreateOrganisationDto("Test Organisation", "123 Test Street", "invalid-email");

            // Act
            var getResult = await httpClient.GetAsync($"/organisations/cd7791c5-a168-4ba7-a24f-143f1c2d95ef");

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetOrganisations_ReturnsAllOrganisation_WhenOrganisationsExists()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            var organisation1 = new CreateOrganisationDto("Test Organisation", "123 Test Street", "test@organisation.com");
            var createdResult = await httpClient.PostAsJsonAsync("/organisations", organisation1);
            var createdOrganisation1 = await createdResult.Content.ReadFromJsonAsync<OrganisationDto>();
            _createdOrganisationIds.Add(createdOrganisation1!.Id);

            var organisation2 = new CreateOrganisationDto("Another Organisation", "456 Another Street", "another@organisation.com");
            var createdResult2 = await httpClient.PostAsJsonAsync("/organisations", organisation2);
            var createdOrganisation2 = await createdResult2.Content.ReadFromJsonAsync<OrganisationDto>();
            _createdOrganisationIds.Add(createdOrganisation2!.Id);

            var organisations = new List<OrganisationDto> { createdOrganisation1, createdOrganisation2 };

            // Act
            var getResult = await httpClient.GetAsync("/organisations");

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var existingOrganisations = await getResult.Content.ReadFromJsonAsync<List<OrganisationDto>>();
            existingOrganisations.Should().BeEquivalentTo(organisations);
        }


        [Fact]
        public async Task GetOrganisations_ReturnsEmptyList_WhenNoOrganisationsExist()
        {
            // Arrange
            var httpClient = _factory.CreateClient();

            // Act
            var getResult = await httpClient.GetAsync("/organisations");

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var existingOrganisations = await getResult.Content.ReadFromJsonAsync<List<OrganisationDto>>();
            existingOrganisations.Should().BeEmpty();
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            var httpClient = _factory.CreateClient();

            foreach (var organisationId in _createdOrganisationIds)
            {
                await httpClient.DeleteAsync($"/organisations/{organisationId}");
            }
        }
    }
}
