using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Controllers;
using MobileStatisticsApp.Core.Entities;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace MobileStatisticsApp.Test;

public class MobileStatisticsTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private HttpClient httpClient;

    private Guid idMobileStatistics = Guid.Parse("27e8b365-325c-463e-b684-faeba1d1c384");

    public MobileStatisticsTest(TestingWebAppFactory<Program> factory)
    {
        httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllMobileStatistics()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var mock = new Mock<ILogger<MobileStatisticsController>>();
        ILogger<MobileStatisticsController> logger = mock.Object;

        logger = Mock.Of<ILogger<MobileStatisticsController>>();
        uowMock.Setup(repo => repo.MobileStatisticsRepository.GetByIdAsync(idMobileStatistics))
            .Returns(Task.FromResult(fakeMobileStatistics));
        var controller = new MobileStatisticsController(uowMock.Object, logger);
        var response = controller.GetById(idMobileStatistics);
        
        Assert.NotNull(response);

        var res = await httpClient.GetAsync("/MobileStatistics");
        Equals(HttpStatusCode.OK, res.StatusCode);
    }

    [Fact]
    public async Task CreateNewMobileStatistics()
    {
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/MobileStatistics");
        var formModel = new MobileStatisticsItem
        {
            Id = Guid.NewGuid(),
            Title = "a1",
            LastStatistics = DateTime.Now,
            VersionClient = "1.2.2",
            Type = "windows"
        };
        var myContent = JsonConvert.SerializeObject(formModel);
        postRequest.Content = new StringContent(myContent, Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(postRequest);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private MobileStatisticsItem fakeMobileStatistics = new MobileStatisticsItem
    {
        Id = Guid.Parse("27e8b365-325c-463e-b684-faeba1d1c384"),
        Title = "a1",
        LastStatistics = DateTime.Now,
        VersionClient = "1.2.2",
        Type = "windows"
    };
}