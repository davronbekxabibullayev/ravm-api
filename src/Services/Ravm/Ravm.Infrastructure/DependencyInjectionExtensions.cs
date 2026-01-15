namespace Ravm.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ravm.Infrastructure.Persistence.EntityFramework.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationPersistence(configuration);

        return services;
    }
}
