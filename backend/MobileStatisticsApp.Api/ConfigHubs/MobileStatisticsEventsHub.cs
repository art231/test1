using Microsoft.AspNetCore.SignalR;
using MobileStatisticsApp.Core.Entities;

namespace MobileStatisticsApp.Api.ConfigHubs;
/// <summary>
/// Хаб запросов для канала связи.
/// </summary>
public class MobileStatisticsEventsHub : Hub
{
    /// <summary>
    /// Отправка сообщения.
    /// </summary>
    /// <param name="hubContext">Контекст отправки сообщения.</param>
    /// <param name="message">Сообщение.</param>
    /// <returns>Выполнение задачи.</returns>
    public static async Task Send(IHubContext<MobileStatisticsEventsHub> hubContext, IEnumerable<MobileStatisticsEvent> message) =>
        await hubContext.Clients.All.SendAsync("TransferData", message);
}
