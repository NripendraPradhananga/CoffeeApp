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
   - Defines interfaces (e.g., `IApiCallCounterService`).
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