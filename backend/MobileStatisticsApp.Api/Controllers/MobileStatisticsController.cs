using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Models;
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
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор для логгирования.
    /// </summary>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/>.</param>
    /// <param name="logger">Сохраняет значение логов.</param>
    public MobileStatisticsController(
        IUnitOfWork unitOfWork,
        ILogger<MobileStatisticsController> logger)
    {
        this.unitOfWork = unitOfWork;
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
        IReadOnlyList<MobileStatisticsItem> statistics = await unitOfWork.MobileStatisticsRepository.GetAllAsync();
        unitOfWork.Commit();
        logger.LogInformation("Get data.");
        var result = statistics.Adapt<List<MobileStatisticsDto>>();
        return await Task.FromResult<IActionResult>(Ok(result));
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
        MobileStatisticsItem statisticsItem = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(id);
        unitOfWork.Commit();
        logger.LogInformation("Get by id Mobile Statistics.");
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
        await unitOfWork.MobileStatisticsRepository.AddAsync(mobileStatistics);
        unitOfWork.Commit();

        return await Task.FromResult<IActionResult>(Ok());
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
        MobileStatisticsItem getItem = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(mobileStatisticsUpdateModel.Id);
        getItem.UpdateMobileStatisticsItem(
            mobileStatisticsUpdateModel.Id,
            mobileStatisticsUpdateModel.Title,
            mobileStatisticsUpdateModel.LastStatistics,
            mobileStatisticsUpdateModel.VersionClient,
            mobileStatisticsUpdateModel.Type
        );
        await unitOfWork.MobileStatisticsRepository.UpdateAsync(getItem);
        unitOfWork.Commit();

        logger.LogInformation("Update mobile statistics.");
        return Ok();
    }
}
