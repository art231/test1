using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.Dtos;
using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Api.Controllers;

/// <summary>
/// Контроллер для мобильной статистики.
/// </summary>
[ApiController]
[Route("[controller]")]
public class MobileStatisticsEventsController : ControllerBase
{
    private readonly ILogger<MobileStatisticsEventsController> logger;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор для событий.
    /// </summary>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/>Хранилище общих репозиториев.</param>
    /// <param name="logger">Сохраняет значение логов.</param>
    public MobileStatisticsEventsController(IUnitOfWork unitOfWork,
        ILogger<MobileStatisticsEventsController> logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    /// <summary>
    /// Получить события по мобильной статистики.
    /// </summary>
    /// <param name="eventId">Идентификатор мобильной статистики.</param>
    /// <returns>Список событий мобильной статистики.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MobileStatisticsEventsDto>))]
    public async Task<IActionResult> GetEventsById(Guid mobileStatisticsId)
    {
        IEnumerable<MobileStatisticsEvent> events = await unitOfWork.MobileStatisticsEventsRepository.GetByIdAsync(mobileStatisticsId);
        MobileStatisticsItem mobileStatistics = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(mobileStatisticsId);
        logger.LogInformation("Get events.");
        var result = new MobileStatisticsWithEventsDto
        {
            Id = mobileStatistics.Id,
            Events = events.Adapt<IEnumerable<MobileStatisticsEventsDto>>(),
        };
        return Ok(result);
    }

    /// <summary>
    /// Создание нового события.
    /// </summary>
    /// <param name="mobileStatisticsEvents">Сущность нового события.</param>
    /// <returns>Ок - если создалось.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEventById(IEnumerable<MobileStatisticsEvent> mobileStatisticsEvents)
    {
        await unitOfWork.MobileStatisticsEventsRepository.CreateEventsAsync(mobileStatisticsEvents);
        logger.LogInformation("Create event.");
        return Ok();
    }
}
