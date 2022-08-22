using Microsoft.Extensions.DependencyInjection;
using MobileStatistics.Application;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.IoC;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IMobileStatisticsRepository, MobileStatisticsRepository>();
        services.AddTransient<IMobileStatisticsEventsRepository, MobileStatisticsEventsRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}