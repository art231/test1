using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MobileStatisticsApp.Api.ConfigHubs;
using MobileStatisticsApp.Api.Dtos;
using MobileStatisticsApp.Api.Models;
using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;

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
    private readonly IMobileStatisticsEventsService mobileStatisticsEventsService;
    /// <summary>
    /// Контроллер.
    /// </summary>
    /// <param name="hub">Хаб.</param>
    /// <param name="logger">Логгер.</param>
    /// <param name="mobileStatisticsEventsService">Сервис.</param>
    public MobileStatisticsEventsController(
        IHubContext<MobileStatisticsEventsHub> hub,
        ILogger<MobileStatisticsEventsController> logger,
        IMobileStatisticsEventsService mobileStatisticsEventsService
        )
    {
        this.hub = hub;
        this.logger = logger;
        this.mobileStatisticsEventsService = mobileStatisticsEventsService;
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

        IEnumerable<MobileStatisticsEvent> events = await this.mobileStatisticsEventsService.GetByIdAsync(mobileStatisticsId);
        if (events.Count().Equals(0))
        {
            return BadRequest("Events is empty.");
        }

        this.logger.LogInformation("Get events.");
        var result = new MobileStatisticsWithEventsDto
        {
            Id = mobileStatisticsId,
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

        await this.mobileStatisticsEventsService.CreateEventsAsync(newListEvents);
        logger.LogInformation("Create event.");
        await MobileStatisticsEventsHub.Send(hub, "Ok");
        return Ok();
    }
}
