using AutoMapper;
using Devhub.Localization.Extensions;
using Microsoft.EntityFrameworkCore;
using Ravm.Api.Extensions;
using Ravm.Api.Middlewares;
using Ravm.Application;
using Ravm.Infrastructure;
using Ravm.Infrastructure.Extensions.DataSeeding;
using Ravm.Infrastructure.Extensions.DataSeeding.Role;
using Ravm.Infrastructure.Persistence.EntityFramework;

Console.Title = "Ravm.Api";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDevhubLocalization(builder.Configuration)
    .AddApplication()
    .AddApplicationApi()
    .AddApplicationInfrastructure(builder.Configuration)
    .AddControllers().Services
    .AddApplicationAuth()
    .AddEndpointsApiExplorer()
    .AddApplicationSwagger()
    .ConfigureSwagger();

var app = builder.Build();

MigrateDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseMiddleware<GlobalMiddlewareErrorHander>();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

//app.MapGroup("account").MapIdentityApi<User>();
app.MapControllers();

app.Run();

static async void MigrateDatabase(IHost host)
{
    using var scope = host.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var provider = scope.ServiceProvider;

    var mapper = provider.GetService<IMapper>();

    db.Database.Migrate();
    db.SeedAsync(mapper);
    new InitialRolesUserSeed()
            .SeedAsync(db, provider)
            .Wait();
}
