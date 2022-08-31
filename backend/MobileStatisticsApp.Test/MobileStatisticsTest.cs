﻿using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Controllers;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Dtos;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace MobileStatisticsApp.Test;

/// <summary>
///     Тесты для мобильной статистики.
/// </summary>
public class MobileStatisticsTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly MobileStatisticsItem fakeMobileStatistics = new()
    {
        Id = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835")
    };

    private readonly HttpClient httpClient;
    private readonly Guid idMobileStatistics = Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835");

    /// <summary>
    ///     Конструктор для создания подключения к проекту.
    /// </summary>
    /// <param name="factory"></param>
    public MobileStatisticsTest(TestingWebAppFactory<Program> factory)
    {
        httpClient = factory.CreateClient();
    }

    /// <summary>
    ///     Проверка для всего списка мобильной статистики.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllMobileStatistics()
    {
        var res = await httpClient.GetAsync("/MobileStatistics");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }

    /// <summary>
    ///     Проверка для создания нового события для мобильной статистики.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    ///     Проверка получения событий по идентификатору.
    /// </summary>
    [Fact]
    public void GetById()
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
        var resultToObject = result!.Value as MobileStatisticsDto;
        Assert.Equal(Guid.Parse("cea30e5b-3171-4ead-820a-53a9d958d835"), resultToObject!.Id);
    }
}