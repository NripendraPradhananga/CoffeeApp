using Application.BrewCoffee.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.BrewCoffee.Services;

public class ApiCallCounterServiceTests
{

    private ApplicationDbContext GetInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task IncrementCounterAsync_ShouldCreateCounter_WhenNoneExists()
    {
        // Arrange
        var applicationDbContext = GetInMemoryDbContext("TestDb1");
        var apiCallCounterService = new ApiCallCounterService(applicationDbContext);

        // Act
        var result = await apiCallCounterService.IncrementCounterAsync(CancellationToken.None);

        // Assert
        Assert.Equal(1, result);
        Assert.Single(applicationDbContext.ApiCallCounters);
        Assert.Equal(1, applicationDbContext.ApiCallCounters.First().Count);
    }

    [Fact]
    public async Task IncrementCounterAsync_ShouldIncrementCounter_WhenCounterExists()
    {
        // Arrange
        var applicationDbContext = GetInMemoryDbContext("TestDb2");
        applicationDbContext.Add(new ApiCallCounter { Id = Guid.NewGuid(), Count = 5 });
        applicationDbContext.SaveChanges();

        var apiCallCounterService = new ApiCallCounterService(applicationDbContext);

        // Act
        var result = await apiCallCounterService.IncrementCounterAsync(CancellationToken.None);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(6, applicationDbContext.ApiCallCounters.First().Count);
    }
}