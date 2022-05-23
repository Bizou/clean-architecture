namespace CleanArchitecture.Application.Features.WeatherForecast.Queries;

using Domain;
using MediatR;

public record GetListQuery : IRequest<IEnumerable<WeatherForecast>>;

public class GetListQueryHandler : RequestHandler<GetListQuery, IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    protected override IEnumerable<WeatherForecast> Handle(GetListQuery request)
    {
        var rng = new Random();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray();
    }
}
