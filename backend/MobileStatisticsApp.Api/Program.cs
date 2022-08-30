using System.Data;
using Mapster;
using MobileStatisticsApp.Api;
using MobileStatisticsApp.Api.Dtos;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.IoC;
using Npgsql;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddInfrastructure();
builder.Host.UseSerilog((hbc, lc) => lc
    .ReadFrom.Configuration(hbc.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
WebApplication app = builder.Build();

var createDb = app.Services.GetRequiredService<DapperDatabase>();
createDb.CreateDatabase("test1");

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

app.UseAuthorization();

app.MapControllers();
TypeAdapterConfig<MobileStatisticsEvent, MobileStatisticsEventsDto>.NewConfig()
    .Map(dest => dest.Date, src => Helper.ConvertToIso(src.Date));
app.Run();

/// <summary>
/// Нужно чтобы видели тесты.
/// </summary>
public partial class Program
{
}
