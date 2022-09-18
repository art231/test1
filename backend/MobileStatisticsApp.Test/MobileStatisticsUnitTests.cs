using System.Data;
using MobileStatisticsApp.Application.Repositories;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Infrastructure;
using MobileStatisticsApp.Repositories;
using Moq;
using Xunit;

namespace MobileStatisticsApp.Test;

/// <summary>
/// Модульные тесты.
/// </summary>
public class MobileStatisticsUnitTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly MobileStatisticsItem fakeMobileStatistics = MobileStatisticsItem.CreateMobileStatisticsItem(
        "a1",
        DateTime.Now,
        "1.2.2",
        "windows");

    private readonly Guid idMobileStatistics = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835");

    /// <summary>
    ///     Проверка получения событий по идентификатору.
    /// </summary>
    [Fact]
    public async Task GetById()
    {
        var mobileStatisticsMock = new Mock<IMobileStatisticsRepository>();
        var mobileStatisticsEventsMock = new Mock<IMobileStatisticsEventsRepository>();
        var dbTransactionMock = new Mock<IDbTransaction>();
        mobileStatisticsMock.Setup(repo => repo.GetByIdAsync(idMobileStatistics))
            .Returns(Task.FromResult(fakeMobileStatistics));
        var uow = new UnitOfWork(dbTransactionMock.Object,
            mobileStatisticsMock.Object, mobileStatisticsEventsMock.Object);
        var response = await uow.MobileStatisticsRepository.GetByIdAsync(idMobileStatistics);
        Assert.Equal("a1", response.Title);
    }
}