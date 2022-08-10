using MobileStatisticsApp.Repositories;

namespace MobileStatistics.Application;

public interface IUnitOfWork
{
    IMobileStatisticsRepository MobileStatistics { get; }
}
