using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatisticsApp.Api.Models;
using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Dtos;

namespace MobileStatisticsApp.Api.Controllers;

/// <summary>
/// Контроллер для мобильной статистики.
/// </summary>
[ApiController]
[Route("[controller]")]
public class MobileStatisticsController : ControllerBase
{
    private readonly ILogger<MobileStatisticsController> logger;

    private readonly IMobileStatisticsService mobileStatisticsService;
    /// <summary>
    /// Конструктор мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsService">сервис мобильной статистики.</param>
    /// <param name="logger">Логгирование.</param>
    public MobileStatisticsController(
        IMobileStatisticsService mobileStatisticsService,
        ILogger<MobileStatisticsController> logger)
    {
        this.mobileStatisticsService = mobileStatisticsService;
        this.logger = logger;
    }

    /// <summary>
    /// Мобильная статистика.
    /// </summary>
    /// <returns>Список мобильной статистики.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MobileStatisticsDto>))]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Get data.");
        IReadOnlyList<MobileStatisticsItem> statistics = await this.mobileStatisticsService.GetAllAsync();
        var result = statistics.Adapt<List<MobileStatisticsDto>>();
        return Ok(result);
    }

    /// <summary>
    /// возвращает статистику отдельного устройства.
    /// </summary>
    /// <param name="id">Уникальный ключ.</param>
    /// <returns>Мобильную статистику устройства.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MobileStatisticsDto))]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("ID is empty.");
        }
        logger.LogInformation("Get by id Mobile Statistics.");
        var statisticsItem = await this.mobileStatisticsService.GetByIdAsync(id);
        var result = statisticsItem.Adapt<MobileStatisticsDto>();
        return Ok(result);
    }

    /// <summary>
    /// Добавление статистики.
    /// </summary>
    /// <param name="mobileStatisticsCreateModel">новые параметры мобильной статистики.</param>
    /// <returns>true если статистика добавилась.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(MobileStatisticsCreateModel mobileStatisticsCreateModel)
    {
        var mobileStatistics = MobileStatisticsItem.CreateMobileStatisticsItem(
            mobileStatisticsCreateModel.Title,
            mobileStatisticsCreateModel.LastStatistics,
            mobileStatisticsCreateModel.VersionClient,
            mobileStatisticsCreateModel.Type);
        logger.LogInformation("Add new mobile statistics.");
        await this.mobileStatisticsService.AddAsync(mobileStatistics);
        return Ok();
    }

    /// <summary>
    /// Обновление мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsUpdateModel">Данные для изменениня.</param>
    /// <returns>Отображение что данные изменились.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMobileStatistics(MobileStatisticsUpdateModel mobileStatisticsUpdateModel)
    {
        MobileStatisticsItem getItem = await this.mobileStatisticsService.GetByIdAsync(mobileStatisticsUpdateModel.Id);
        getItem.UpdateMobileStatisticsItem(
            mobileStatisticsUpdateModel.Id,
            mobileStatisticsUpdateModel.Title,
            mobileStatisticsUpdateModel.LastStatistics,
            mobileStatisticsUpdateModel.VersionClient,
            mobileStatisticsUpdateModel.Type
        );
        await this.mobileStatisticsService.UpdateAsync(getItem);

        logger.LogInformation("Update mobile statistics.");
        return Ok();
    }
}
