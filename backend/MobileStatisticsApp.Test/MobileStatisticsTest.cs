using System.Net;
using System.Text;
using MobileStatisticsApp.Core.Entities;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Controllers;
using MobileStatisticsApp.Dtos;
using Moq;

namespace MobileStatisticsApp.Test;

public class MobileStatisticsTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private HttpClient httpClient;
    private Guid idMobileStatistics = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835");


    public MobileStatisticsTest(TestingWebAppFactory<Program> factory)
    {
        httpClient = factory.CreateClient();
    }


    [Fact]
    public async Task GetAllMobileStatistics()
    {
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
        Equals(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var mock = new Mock<ILogger<MobileStatisticsController>>();
        var logger = mock.Object;

        logger = Mock.Of<ILogger<MobileStatisticsController>>();
        uowMock.Setup(repo => repo.MobileStatisticsRepository.GetByIdAsync(idMobileStatistics))
            .Returns(Task.FromResult(fakeMobileStatistics));
        var controller = new MobileStatisticsController(uowMock.Object, logger);
        var response = controller.GetById(idMobileStatistics);
        var result = response.Result as OkObjectResult;
        var resultToObject = result.Value as MobileStatisticsDto;
        Equals(Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835"), resultToObject.Id);
    }

    private MobileStatisticsItem fakeMobileStatistics = new()
    {
        Id = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835")
    };
}