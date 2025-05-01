using Application.Abstraction;
using Application.BrewCoffee;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.BrewCoffee;

public class GetBrewCoffeeQueryHandlerTests
{
    private readonly Mock<ILogger<GetBrewCoffeeQueryHandler>> _mockLogger;
    private readonly Mock<IApiCallCounterService> _mockApiCallCounterService;
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    private readonly Mock<IWeatherService> _mockWeatherService;
    private readonly GetBrewCoffeeQueryHandler _handler;

    public GetBrewCoffeeQueryHandlerTests()
    {
        _mockLogger = new Mock<ILogger<GetBrewCoffeeQueryHandler>>();
        _mockApiCallCounterService = new Mock<IApiCallCounterService>();
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _mockWeatherService = new Mock<IWeatherService>();
        _handler = new GetBrewCoffeeQueryHandler(
            _mockLogger.Object,
            _mockApiCallCounterService.Object,
            _mockDateTimeProvider.Object,
            _mockWeatherService.Object
        );
    }

   
    [Fact]
    public async Task Handle_ShouldReturnTeapot_WhenDateIsFirstOfApril()
    {
        // Arrange
        _mockDateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2025, 4, 1));

        // Act
        var result = await _handler.Handle(new GetBrewCoffeeQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(BrewCoffeeStatus.Teapot, result.Status);
        Assert.Null(result.Message);
        Assert.Null(result.Prepared);
    }


    [Fact]
    public async Task Handle_ShouldReturnServiceUnavailable_WhenCounterIsDivisibleByThreshold()
    {
        // Arrange
        _mockDateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2025, 3, 31));
        _mockApiCallCounterService.Setup(x => x.IncrementCounterAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        // Act
        var result = await _handler.Handle(new GetBrewCoffeeQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(BrewCoffeeStatus.ServiceUnavailable, result.Status);
        Assert.Null(result.Message);
        Assert.Null(result.Prepared);
    }


    [Fact]
    public async Task Handle_ShouldReturnBrewed_WhenConditionsAreNormal()
    {
        // Arrange
        _mockDateTimeProvider.Setup(x => x.Now).Returns(new DateTime(2025, 3, 31));
        _mockApiCallCounterService.Setup(x => x.IncrementCounterAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(new GetBrewCoffeeQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(BrewCoffeeStatus.Brewed, result.Status);
        Assert.Equal("Your piping hot coffee is ready", result.Message);
        Assert.NotNull(result.Prepared);
    }
}
