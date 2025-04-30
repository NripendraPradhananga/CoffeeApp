using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender;

    // Lazy loading the ISender instance from the service provider
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult CreateEmptyResponse(int statusCode)
    {
        Response.StatusCode = statusCode;
        Response.ContentLength = 0;
        return new EmptyResult();
    }
}
