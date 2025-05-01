# CoffeeApp
CoffeeApp is a simple Web API application that mimics the functionality of a coffee machine. It has one endpoint `GET /brew-coffee` with functionalities as follows:
1. When GET /brew-coffee is called, it returns 200 OK with a response 
```json
{
    “message”: “Your piping hot coffee is ready”,
    “prepared”: “2021-02-03T11:56:24+0900”
};
```
2. On every 5th call, it returns 503 Service Unavailable with an empty response
3. If the date is 1st of April, it returns 418 I’m a teapot with an empty response
4. If the temperature is above 30 degrees Celsius, it returns 200 OK with a response 
```json
{
    “message”: “Your refreshing iced coffee is ready”,
    “prepared”: “2021-02-03T11:56:24+0900”
};
```


The application is built using ASP.NET Core and follows the **Clean Architecture** principles. It also uses an **In-Memory Database** for data storage for demonstration purposes, which can be easily extended to other databases if required.


---

## Clean Architecture

CoffeeApp is designed following the Clean Architecture principles to ensure a clear separation of concerns and maintainability. The architecture is divided into the following layers:

1. Domain Layer:
   - The core of the application
   - Includes entities, value objects, and domain-specific logic.
   - This layer is completely independent of other layers, ensuring that it can evolve without being affected by external dependencies.

2. Application Layer:
   - Contains the core business logic and application-specific rules.
   - efines interfaces (e.g., `IWeatherService`, `IApiCallCounterService`) that are implemented in the Infrastructure layer.
   - Uses MediatR for handling CQRS patterns (e.g., `GetBrewCoffeeQuery`).

3. Infrastructure Layer:
   - Contains implementations of interfaces defined in the Application layer.
   - Handles external concerns such as HTTP calls and data persistence.

4. WebUI Layer:
   - The entry point of the application.
   - Handles HTTP requests and responses.
   - Configures dependency injection and middleware.

This architecture ensures that the core business logic is independent of external frameworks or technologies, making the application easier to test, maintain, and extend.

---

## In-Memory Database

CoffeeApp uses an In-Memory Database for demonstration purposes. This database is lightweight and does not require any external setup, making it ideal for development and testing. 

### Key Features:
- Ephemeral Storage: Data is stored in memory and is lost when the application stops.
- Ease of Use: No additional configuration or setup is required.
- Extensibility: The in-memory database can be replaced with a relational database (e.g., SQL Server, PostgreSQL) or a NoSQL database with minimal changes.

### Usage in CoffeeApp:
- The in-memory database is used to store and manage the API call counter, which tracks the number of times the `/brew-coffee` endpoint is called.
- This setup demonstrates how the application can interact with a database while adhering to Clean Architecture principles.

---
## OpenWeather API Integration

CoffeeApp uses the OpenWeather API to fetch weather data and displays it in a user-friendly format. The weather data is used to determine whether to serve hot coffee or iced coffee based on the temperature.

### Configuration:
Add your OpenWeather API key and other settings in the `appsettings.json` file:

It uses the OpenWeather API to fetch weather data and displays it in a user-friendly format.

OpenWeather API is used to get the weather data. The API key is required to access the data. You can sign up for a free account at [OpenWeather](https://openweathermap.org/api) to get your API key.

```
  "OpenWeather": {
    "ApiKey": "",
    "BaseUrl": "https://api.openweathermap.org/data/2.5/weather",
    "City": "Hamilton,NZ",
    "Unit": "metric"
  }
```

How to run the project

```
dotnet run --project ./src/WebUI/WebUI.csproj
```

How to run the tests

```
dotnet test
```


# Future Enhancements
1. Use Transactions for Database Operations
2. Improve Error Handling
3. Add Caching
- Use an in-memory cache like IMemoryCache or a distributed cache like Redis.
4. Add Logging and Monitoring