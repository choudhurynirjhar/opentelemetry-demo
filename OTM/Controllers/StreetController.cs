using Microsoft.AspNetCore.Mvc;

namespace OTM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreetController : ControllerBase
{
    private readonly HttpClient client;
    private readonly IUserProvider userProvider;

    public StreetController(HttpClient client, IUserProvider userProvider)
    {
        this.client = client;
        this.userProvider = userProvider;
    }
    // GET: api/<StreetController>
    [HttpGet]
    public async Task<IEnumerable<string>> Get()
    {
        var users = userProvider.Get();
        var response = await client.GetAsync("https://localhost:7283/WeatherForecast?name=test");
        return new string[] { "value1", "value2" };
    }
}