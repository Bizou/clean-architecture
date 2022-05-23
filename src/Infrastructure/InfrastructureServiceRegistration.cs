namespace CleanArchitecture.Infrastructure;

using Application.Contracts.Persistance;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

        services
            .AddScoped<IAsyncRepository<TodoItem>, TodoRepository>();

        return services;
    }
}
