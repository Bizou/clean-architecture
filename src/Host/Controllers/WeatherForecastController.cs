namespace CleanArchitecture.Host.Controllers;

using Application.Features.WeatherForecast.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ISender sender;

    public WeatherForecastController(ISender sender) => this.sender = sender;

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken = default) =>
        await this.sender.Send(new GetListQuery(), cancellationToken);
}
