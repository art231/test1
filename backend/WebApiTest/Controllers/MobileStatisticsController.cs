using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    /// <summary>
    /// Контроллер для мобильной статистики.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MobileStatisticsController : ControllerBase
    {
        private static readonly MobileStatistics[] statistics = new MobileStatistics[3] {
            new MobileStatistics {Id=1, Title="a123",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=2, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=3, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"}};

        private readonly ILogger<MobileStatisticsController> _logger;
        /// <summary>
        /// Конструктор для логгирования.
        /// </summary>
        /// <param name="logger">Сохраняет значение логов.</param>
        public MobileStatisticsController(ILogger<MobileStatisticsController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Мобильная статистика.
        /// </summary>
        /// <returns>Список мобильной статистики.</returns>
        [HttpGet(Name = "MobileStatistics")]
        public IEnumerable<MobileStatistics> Get()
        {
            _logger.LogInformation("--Log Information--");
            return statistics;
        }
    }
}
