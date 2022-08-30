using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatistics.Application;
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
    public MobileStatisticsController(IUnitOfWork unitOfWork,
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
        IReadOnlyList<MobileStatisticsItem>? statistics = await unitOfWork.MobileStatisticsRepository.GetAllAsync();
        unitOfWork.Commit();
        logger.LogInformation("Get data.");
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
        MobileStatisticsItem? statisticsItem = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(id);
        unitOfWork.Commit();
        logger.LogInformation("Get by id Mobile Statistics.");
        var result = statisticsItem.Adapt<MobileStatisticsDto>();
        return Ok(result);
    }

    /// <summary>
    /// Добавление статистики.
    /// </summary>
    /// <param name="mobileStatistics">новые параметры мобильной статистики.</param>
    /// <returns>true если статистика добавилась.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(MobileStatisticsItem mobileStatistics)
    {
        logger.LogInformation("Add new mobile statistics.");
        mobileStatistics.Id = Guid.NewGuid();
        await unitOfWork.MobileStatisticsRepository.AddAsync(mobileStatistics);
        unitOfWork.Commit();
        return Ok();
    }

    /// <summary>
    /// Обновление мобильной статистики.
    /// </summary>
    /// <param name="mobileStatistics">Данные для изменениня.</param>
    /// <returns>Отображение что данные изменились.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMobileStatistics(MobileStatisticsItem mobileStatistics)
    {
        await unitOfWork.MobileStatisticsRepository.UpdateAsync(mobileStatistics);
        unitOfWork.Commit();
        logger.LogInformation("Update mobile statistics.");
        return Ok();
    }
}
