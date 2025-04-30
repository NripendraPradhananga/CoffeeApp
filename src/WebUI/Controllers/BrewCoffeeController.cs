using Application.BrewCoffee;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts;

namespace WebUI.Controllers;

[ApiController]
[Route("brew-coffee")]
public class BrewCoffeeController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await Sender.Send(new GetBrewCoffeeQuery());
        return response.Status switch
        {
            BrewCoffeeStatus.Brewed => Ok(new GetBrewCoffeeResponse(
                response.Message!,
                response.Prepared?.ToString("o")!
            )),

            BrewCoffeeStatus.ServiceUnavailable => CreateEmptyResponse(StatusCodes.Status503ServiceUnavailable),

            BrewCoffeeStatus.Teapot => CreateEmptyResponse(StatusCodes.Status418ImATeapot),

            _ => BadRequest()
        };
    }
}
