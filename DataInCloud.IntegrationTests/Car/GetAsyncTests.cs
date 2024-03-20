using System.Net;
using System.Text;
using DataInCloud.Dal.Car;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace DataInCloud.IntegrationTests.Car;

public class GetAsyncTests : BaseTest
{
    private readonly HttpClient _httpClient;
    public GetAsyncTests()
    {
        _httpClient = InitTestServer().GetClient();
    }

    [Fact]
    public async Task GetAsync_ReturnsAllEntities()
    {
        // Arrange 
        AppDbContext.Cars.Add(new CarDao
        {
            DoorsCount = 1,
            IsBuyEnable = false,
            Name = "TestName"
        });
        await AppDbContext.SaveChangesAsync();

        // Act
        var message = new HttpRequestMessage(HttpMethod.Get, "api/v1/cars/1");
        var response = await _httpClient.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}