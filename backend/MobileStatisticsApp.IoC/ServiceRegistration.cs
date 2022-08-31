using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.Repositories;
namespace MobileStatisticsApp.IoC;

using Microsoft.Extensions.DependencyInjection;

using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
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
        services.AddSingleton<DapperDatabase>();
        services.AddTransient<IMobileStatisticsRepository, MobileStatisticsRepository>();
        services.AddTransient<IMobileStatisticsEventsRepository, MobileStatisticsEventsRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}