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
    /// Конструктор для логгирования.
    /// </summary>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/>.</param>
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
    /// <param name="id">Идентификатор мобильной статистики.</param>
    /// <returns>Список событий мобильной статистики.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MobileStatisticsEventsDto>))]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        IEnumerable<MobileStatisticsEvents> events = await unitOfWork.MobileStatisticsEventsRepository.GetByIdAsync(id);
        MobileStatisticsItem mobileStatistics = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(id);
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
    /// <param name="mobileStatisticsEvent">Сущность нового события.</param>
    /// <returns>Ок - если создалось.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateEventById(MobileStatisticsEvents mobileStatisticsEvent)
    {
        unitOfWork.MobileStatisticsEventsRepository.CreateEvent(mobileStatisticsEvent);
        logger.LogInformation("Create event.");
        return Ok();
    }

    /// <summary>
    /// Обновление событий мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsEvents">Сущность события.</param>
    /// <returns>Ок - если успешно.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateEvents(MobileStatisticsEvents mobileStatisticsEvents)
    {
        unitOfWork.MobileStatisticsEventsRepository.Update(mobileStatisticsEvents);

        logger.LogInformation("Update mobile statistics events.");
        return Ok();
    }
}
