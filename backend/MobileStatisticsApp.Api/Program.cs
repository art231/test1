using Serilog;
using MobileStatisticsApp.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();
