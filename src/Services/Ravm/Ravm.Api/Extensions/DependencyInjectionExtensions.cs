namespace Ravm.Api.Extensions;

using System.Reflection;
using Ravm.Api.Services;
using Ravm.Application.Common;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddApplicationApiCORS();

        services.AddTransient<IAuthService, AuthService>();
        services.AddScoped<IWaybillCertificateGenerator, WaybillCertificateGenerator>();
        services.AddTransient<IUserService, UserService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
