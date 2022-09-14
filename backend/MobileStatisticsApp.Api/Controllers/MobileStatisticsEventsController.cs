using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MobileStatistics.Application;
using MobileStatisticsApp.Api.ConfigHubs;
using MobileStatisticsApp.Api.Dtos;
using MobileStatisticsApp.Api.Models;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Infrastructure;

namespace MobileStatisticsApp.Api.Controllers;

/// <summary>
/// Контроллер для мобильной статистики.
/// </summary>
[ApiController]
[Route("[controller]")]
public class MobileStatisticsEventsController : ControllerBase
{
    private readonly IHubContext<MobileStatisticsEventsHub> hub;
    private readonly ILogger<MobileStatisticsEventsController> logger;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    /// Конструктор для событий.
    /// </summary>
    /// <param name="unitOfWork"><see cref="IUnitOfWork"/>Хранилище общих репозиториев.</param>
    /// <param name="logger">Сохраняет значение логов.</param>
    /// <param name="hub">Сохраняет значение логов.</param>
    public MobileStatisticsEventsController(
        IHubContext<MobileStatisticsEventsHub> hub,
        IUnitOfWork unitOfWork,
        ILogger<MobileStatisticsEventsController> logger)
    {
        this.hub = hub;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    /// <summary>
    /// Получить события по мобильной статистики.
    /// </summary>
    /// <param name="mobileStatisticsId">Идентификатор мобильной статистики.</param>
    /// <returns>Список событий мобильной статистики.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MobileStatisticsEventsDto>))]
    public async Task<IActionResult> GetEventsById(Guid mobileStatisticsId)
    {
        if (mobileStatisticsId == Guid.Empty)
        {
            return BadRequest("ID is empty.");
        }

        IEnumerable<MobileStatisticsEvent> events = await unitOfWork.MobileStatisticsEventsRepository.GetByIdAsync(mobileStatisticsId);
        MobileStatisticsItem mobileStatistics = await unitOfWork.MobileStatisticsRepository.GetByIdAsync(mobileStatisticsId);
        unitOfWork.Commit();
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
    /// <param name="mobileStatisticsEventsCreateModel">Сущность нового события.</param>
    /// <returns>Ок - если создалось.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateEventById(IEnumerable<CreateMobileStatisticsEventModel> mobileStatisticsEventsCreateModel)
    {
        var newListEvents = new List<MobileStatisticsEvent>();
        foreach (CreateMobileStatisticsEventModel @event in mobileStatisticsEventsCreateModel)
        {
            newListEvents.Add(MobileStatisticsEvent.CreateNewEvent(
                @event.MobileStatisticsId,
                @event.Date,
                @event.Name,
                @event.Description));
        }

        await unitOfWork.MobileStatisticsEventsRepository.CreateEventsAsync(newListEvents);
        unitOfWork.Commit();
        logger.LogInformation("Create event.");
        await MobileStatisticsEventsHub.Send(hub, newListEvents);
        return Ok();
    }
}
