using banking_api;
using banking_api.Endpoints.DataTransferObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace banking_api_tests_integration
{
    public class OrganisationEndpointsTests : IClassFixture<WebApplicationFactory<IApiMarker>>
    {
        private readonly WebApplicationFactory<IApiMarker> _factory;

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
            var createdOrganisation = await result.Content.ReadFromJsonAsync<OrganisationDto>();

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
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
            var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationError>>();
            var error = errors!.Single();

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.PropertyName.Should().Be("Email");
            error.ErrorMessage.Should().Be("Organisation email is not valid.");
        }
    }
}
