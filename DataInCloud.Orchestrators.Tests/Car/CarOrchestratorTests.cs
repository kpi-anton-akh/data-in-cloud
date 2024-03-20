using System.Net.Http.Headers;
using DataInCloud.Model.Car;
using DataInCloud.Orchestrators.Car;
using FluentAssertions;
using Moq;

namespace DataInCloud.Orchestrators.Tests.Car;

public class CarOrchestratorTests
{
    [Fact]
    public async Task GetByIdAsync_IfExists_ReturnsCar()
    {
        // Arrange
        const int carId = 10101001;
        var existingCar = new Model.Car.Car
        {
            Id = carId,
            IsBuyEnable = true,
            DoorsCount = 10,
            Name = "FakeCarName"
        };
        var repository = new Mock<ICarRepository>();
        repository.Setup(r => r.GetByIdAsync(carId))
            .ReturnsAsync(existingCar);
        var orchestrator = new CarOrchestrator(repository.Object);

        // Act
        var result = await orchestrator.GetByIdAsync(carId);

        // Assert
        result.Should().Be(existingCar);
    }
}