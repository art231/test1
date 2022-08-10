using MobileStatisticsApp.Repositories;

namespace MobileStatistics.Application.Repositories
{
    public interface IUnitOfWork
    {
        IMobileStatisticsRepository MobileStatistics { get; }
    }
}
