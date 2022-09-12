using System.Net;
using System.Text;
using MobileStatisticsApp.Core.Entities;
using Newtonsoft.Json;
using Xunit;

namespace MobileStatisticsApp.Test;

/// <summary>
/// Интеграционные тесты для мобильной статистики.
/// </summary>
public class MobileStatisticsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private HttpClient httpClient;

    private MobileStatisticsItem fakeModel = new()
    {
        Id = Guid.NewGuid(),
        Title = "a1",
        LastStatistics = DateTime.Now,
        VersionClient = "1.2.2",
        Type = "windows"
    };
    /// <summary>
    /// Конструктор для создания подключения к проекту.
    /// </summary>
    /// <param name="factory"></param>
    public MobileStatisticsIntegrationTests(TestingWebAppFactory<Program> factory)
    {
        httpClient = factory.CreateClient();
    }

    /// <summary>
    /// Проверка для создания нового события для мобильной статистики.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNewMobileStatistics()
    {
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/MobileStatistics");

        var myContent = JsonConvert.SerializeObject(fakeModel);
        postRequest.Content = new StringContent(myContent, Encoding.UTF8, "application/json");

        var responseCreate = await httpClient.SendAsync(postRequest);
        Assert.Equal(HttpStatusCode.OK, responseCreate.StatusCode);
    }

    /// <summary>
    /// Проверка для всего списка мобильной статистики.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllMobileStatistics()
    {
        //Создание мобильной статистики.
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/MobileStatistics");

        var myContent = JsonConvert.SerializeObject(fakeModel);
        postRequest.Content = new StringContent(myContent, Encoding.UTF8, "application/json");

        var resultCreate = await httpClient.SendAsync(postRequest);
        Assert.Equal(HttpStatusCode.OK, resultCreate.StatusCode);
    }
    /// <summary>
    /// Проверка по имени и по дате.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetByNameAndDate()
    {
        var res = await httpClient.GetAsync("/MobileStatistics");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);

        var content = await res.Content.ReadAsStringAsync();
        var responseData = JsonConvert.DeserializeObject<MobileStatisticsItem[]>(content)!;
        var resultFromDb = responseData.Where(x => x.LastStatistics.Date == fakeModel.LastStatistics.Date
                                                   && x.Title == fakeModel.Title)!.FirstOrDefault();
        Assert.True(Equal(resultFromDb!, fakeModel));
    }

    private static bool Equal(MobileStatisticsItem result, MobileStatisticsItem createModel)
    {
        return result.LastStatistics.EqualWithoutHours(createModel.LastStatistics)
               && result?.Title == createModel?.Title
               && result?.Type == createModel?.Type
               && result?.VersionClient == createModel?.VersionClient;
    }
}