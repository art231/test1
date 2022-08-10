using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileStatistics.Application;
using MobileStatistics.Infrastructure;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.IoC;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IMobileStatisticsRepository, MobileStatisticsRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}