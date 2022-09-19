using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.Infrastructure.Services;
using MobileStatisticsApp.Repositories;
namespace MobileStatisticsApp.IoC;

using Microsoft.Extensions.DependencyInjection;
/// <summary>
/// Регистрация сервисов.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Дополнительная регистрация сервисов.
    /// </summary>
    /// <param name="services">Общая коллекция сервисов программы.</param>
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<DapperDatabase>();
        services.AddScoped<IMobileStatisticsRepository, MobileStatisticsRepository>();
        services.AddScoped<IMobileStatisticsEventsRepository, MobileStatisticsEventsRepository>();
        services.AddScoped<IMobileStatisticsEventsService, MobileStatisticsEventsService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDalSession, DalSession>();
        services.AddScoped<IMobileStatisticsService, MobileStatisticsService>();
    }
}