using Application.BrewCoffee;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebUI.Contracts;
using WebUI.Controllers;

namespace WebUI.Tests.Controllers;

public class BrewCoffeeControllerTests
{
    private readonly Mock<ISender> _mockSender;
    private readonly BrewCoffeeController _brewCoffeeController;

    public BrewCoffeeControllerTests()
    {
        _mockSender = new Mock<ISender>();
        _brewCoffeeController = new BrewCoffeeController();

        var mockHttpContext = new DefaultHttpContext();
        _brewCoffeeController.ControllerContext = new ControllerContext
        {
            HttpContext = mockHttpContext
        };

        // Use reflection to set the private _sender field
        var senderField = typeof(ApiControllerBase).GetField("_sender", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        senderField?.SetValue(_brewCoffeeController, _mockSender.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnOk_WhenStatusIsBrewed()
    {
        // Arrange
        var message = "Your coffee is ready!";
        var prepared = DateTime.Now;

        var response = new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.Brewed, message, prepared);
        _mockSender.Setup(s => s.Send(It.IsAny<GetBrewCoffeeQuery>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(response);

        // Act
        var result = await _brewCoffeeController.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.IsType<GetBrewCoffeeResponse>(okResult.Value);


        Assert.Equal(message, ((GetBrewCoffeeResponse) okResult.Value).Message);
        Assert.Equal(prepared.ToString("o"), ((GetBrewCoffeeResponse)okResult.Value).Prepared);
    }

    [Fact]
    public async Task Get_ShouldReturnServiceUnavailable_WhenStatusIsServiceUnavailable()
    {
        // Arrange
        var response = new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.ServiceUnavailable, null, null);
        _mockSender.Setup(s => s.Send(It.IsAny<GetBrewCoffeeQuery>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(response);

        // Act
        var result = await _brewCoffeeController.Get();

        // Assert
        Assert.Equal(StatusCodes.Status503ServiceUnavailable, _brewCoffeeController.Response.StatusCode);
        Assert.IsType<EmptyResult>(result);
        
    }

    [Fact]
    public async Task Get_ShouldReturnTeapot_WhenStatusIsTeapot()
    {
        // Arrange
        var response = new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.Teapot, null, null);
        _mockSender.Setup(s => s.Send(It.IsAny<GetBrewCoffeeQuery>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(response);

        // Act
        var result = await _brewCoffeeController.Get();

        // Assert
        Assert.IsType<EmptyResult>(result);
        Assert.Equal(StatusCodes.Status418ImATeapot, _brewCoffeeController.Response.StatusCode);
    }
}