using banking_api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace banking_api_tests_unit.Models
{
    public class OrganisationTests
    {
        [Fact]
        public void CreateOrganisation_ShouldInitializeProperties()
        {
            // Arrange
            var name = "Test Organisation";
            var address = "123 Test Street";
            var email = "test@organisation.com";

            // Act
            var organisation = Organisation.Create(name, address, email);

            // Assert  
            Assert.IsType<Organisation>(organisation);
            Assert.Equal(name, organisation.Name);
            Assert.Equal(address, organisation.Address);
            Assert.Equal(email, organisation.Email);
            Assert.IsType<DateTimeOffset>(organisation.CreatedAt);
            Assert.IsType<DateTimeOffset>(organisation.UpdatedAt);
        }

        [Fact]
        public void UpdateOrganisation_ShouldUpdatePropertiesAndUpdatedAt()
        {
            // Arrange
            var organisation = Organisation.Create("Test Organisation", "123 Test Street", "test@organisation.com");
            var originalCreatedAt = organisation.CreatedAt;
            var originalUpdatedAt = organisation.UpdatedAt;

            // Act
            organisation.Update("Updated Organisation", "456 Updated Street", "updated@organisation.com");

            // Assert  
            Assert.IsType<Organisation>(organisation);
            Assert.Equal("Updated Organisation", organisation.Name);
            Assert.Equal("456 Updated Street", organisation.Address);
            Assert.Equal("updated@organisation.com", organisation.Email);
            Assert.Equal(originalCreatedAt, organisation.CreatedAt);
            Assert.NotEqual<DateTimeOffset>(originalUpdatedAt, organisation.UpdatedAt);
        }

    }
}
