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
    private readonly MobileStatisticsItem fakeMobileStatistics = new()
    {
        Id = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835")
    }; 
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
        Assert.Equal(Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835"), response.Id);
    }
}