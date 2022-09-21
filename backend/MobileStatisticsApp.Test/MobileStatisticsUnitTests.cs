using MobileStatisticsApp.Core.Entities;
using Xunit;
//using MobileStatisticsApp.Application.Repositories;

namespace MobileStatisticsApp.Test;

/// <summary>
///     Модульные тесты.
/// </summary>
public class MobileStatisticsUnitTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly MobileStatisticsItem fakeMobileStatistics = MobileStatisticsItem.CreateMobileStatisticsItem(
        "a1",
        DateTime.Now,
        "1.2.2",
        "windows");

    private readonly Guid idMobileStatistics = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835");
}