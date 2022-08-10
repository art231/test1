using MobileStatistics.Application;
using MobileStatisticsApp.Repositories;

namespace MobileStatistics.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IMobileStatisticsRepository mobileStatisticsRepository)
    {
        MobileStatistics = mobileStatisticsRepository;
    }
    public IMobileStatisticsRepository MobileStatistics { get; }
}
