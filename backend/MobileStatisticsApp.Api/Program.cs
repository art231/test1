using System.Data;
using Mapster;
using MobileStatisticsApp.Api;
using MobileStatisticsApp.Api.ConfigHubs;
using MobileStatisticsApp.Api.Dtos;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.IoC;
using Npgsql;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(s =>
{
    var conn = s.GetRequiredService<IDbConnection>();
    conn.Open();
    return conn.BeginTransaction();
});
builder.Services.AddInfrastructure();
builder.Host.UseSerilog((hbc, lc) => lc
    .ReadFrom.Configuration(hbc.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(h =>
{
    h.MaximumReceiveMessageSize = 102400000;
    h.EnableDetailedErrors = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
WebApplication app = builder.Build();

var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (IServiceScope scope = serviceScopeFactory!.CreateScope())
{
    var createDb = scope.ServiceProvider.GetRequiredService<DapperDatabase>();
    createDb.CreateDatabase(builder.Configuration["CreateDatabase"]);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSerilogRequestLogging();
    app.UseReDoc(c =>
    {
        c.DocumentTitle = "REDOC API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());
}

app.MapHub<MobileStatisticsEventsHub>("/mobileStatisticsHub");
app.UseAuthorization();

app.MapControllers();

//Настройка для времени события.
TypeAdapterConfig<MobileStatisticsEvent, MobileStatisticsEventsDto>.NewConfig()
    .Map(dest => dest.Date, src => Helper.ConvertToIso(src.Date));

//Настройка для даппера
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
app.Run();

/// <summary>
/// Нужно чтобы видели тесты.
/// </summary>
public partial class Program
{
}
