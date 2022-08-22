using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Repositories;
/// <summary>
/// Прослойка для конкретного репозитария.
/// </summary>
public interface IMobileStatisticsRepository : IGenericRepository<MobileStatisticsItem>
{
}