using System.Net;
using System.Text;
using DataInCloud.Orchestrators.Car.Contract;
using EntityFrameworkCore.Testing.Common.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DataInCloud.IntegrationTests.Car;

public class PostAsyncTests : BaseTest
{
    private readonly HttpClient _httpClient;
    public PostAsyncTests()
    {
        _httpClient = InitTestServer().GetClient();
    }

    [Fact]
    public async Task PostAsync_ToBigDoorsCount_ReturnsBadRequest()
    {
        // Arrange
        var inputModel = new CreateCar
        {
            DoorsCount = 101,
            IsBuyEnable = true,
            Name = "Super Car 2024"
        };
        var message = new HttpRequestMessage(HttpMethod.Post, "api/v1/cars");
        message.Content = new StringContent(JsonConvert.SerializeObject(inputModel), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.SendAsync(message);


        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
