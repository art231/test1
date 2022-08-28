using Microsoft.Extensions.Logging;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Controllers;
using MobileStatisticsApp.Core.Entities;
using Moq;
using Xunit;

namespace MobileStatisticsApp.Test
{
    public class MobileStatisticsEventsTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private Guid idMobileStatistics = Guid.Parse("27e8b365-325c-463e-b684-faeba1d1c384");
        
        [Fact]
        public async Task GetEventsById()
        {
            var uowMock = new Mock<IUnitOfWork>();
            var mock = new Mock<ILogger<MobileStatisticsEventsController>>();
            ILogger<MobileStatisticsEventsController> logger = mock.Object;

            //logger = Mock.Of<ILogger<MobileStatisticsEventsController>>();
            uowMock.Setup(repo => repo.MobileStatisticsEventsRepository.GetByIdAsync(idMobileStatistics))
                .Returns(Task.FromResult(fakeMobileStatisticsEvent));
            var controller = new MobileStatisticsEventsController(uowMock.Object, logger);
            var response = controller.GetEventsById(idMobileStatistics);

            Assert.NotNull(response);
        }

        private IEnumerable<MobileStatisticsEvent> fakeMobileStatisticsEvent = new List<MobileStatisticsEvent>
        {
            new MobileStatisticsEvent
            {
                Id = Guid.Parse("27e8b365-325c-463e-b684-faeba1d1c384"),
                Name = "dsds",
                Date = DateTime.Now,
                Description = "dsdasdsa"
            }
        };
    }
}
