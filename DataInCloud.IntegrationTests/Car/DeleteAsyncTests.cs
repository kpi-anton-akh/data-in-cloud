using System.Net;
using DataInCloud.Dal.Car;
using FluentAssertions;
using Xunit;

namespace DataInCloud.IntegrationTests.Car;

public class DeleteAsyncTests : BaseTest
{
    private readonly HttpClient _httpClient;
    public DeleteAsyncTests()
    {
        _httpClient = InitTestServer().GetClient();
    }

    [Fact]
    public async Task DeleteAsync_IfRecordExists_RemovesEntityFromDb()
    {
        // Arrange
        var carToDelete = new CarDao
        {
            Name = "SeedCar to delete",
            DoorsCount = 5,
            IsBuyEnable = true,
            Id = 100001
        };
        await AppDbContext.Cars.AddAsync(carToDelete);
        await AppDbContext.SaveChangesAsync();
        var message = new HttpRequestMessage(HttpMethod.Delete, $"api/v1/cars/{carToDelete.Id}");

        // Act
        var response = await _httpClient.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        AppDbContext.Cars.FirstOrDefault(c => c.Id == carToDelete.Id).Should().BeNull();
    }
}
