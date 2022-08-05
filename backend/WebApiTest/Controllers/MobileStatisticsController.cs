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
        private static readonly List<MobileStatistics> statistics = new List<MobileStatistics> {
            new MobileStatistics {Id=Guid.NewGuid(), Title="a123",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=Guid.NewGuid(), Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=Guid.NewGuid(), Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"}};

        private readonly ILogger<MobileStatisticsController> logger;
        /// <summary>
        /// Конструктор для логгирования.
        /// </summary>
        /// <param name="logger">Сохраняет значение логов.</param>
        public MobileStatisticsController(ILogger<MobileStatisticsController> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// Мобильная статистика.
        /// </summary>
        /// <returns>Список мобильной статистики.</returns>
        [HttpGet]
        public IEnumerable<MobileStatistics> Get()
        {
            this.logger.LogInformation("--Log Information--");
            return statistics;
        }
        /// <summary>
        /// возвращает статистику отдельного устройства.
        /// </summary>
        /// <param name="id">Уникальный ключ.</param>
        /// <returns>Мобильную статистику устройства.</returns>
        [HttpGet("{id}")]
        public MobileStatistics GetById(Guid id)
        {
            return statistics.FirstOrDefault(x => x.Id == id)!;
        }
        /// <summary>
        /// Добавление статистики.
        /// </summary>
        /// <param name="mobileStatistics">новые параметры мобильной статистики.</param>
        /// <returns>true если статистика добавилась.</returns>
        [HttpPost]
        public bool Add(MobileStatistics mobileStatistics)
        {
            mobileStatistics.Id = Guid.NewGuid();
            statistics.Add(mobileStatistics);
            return true;
        }
        /// <summary>
        /// Обновление мобильной статистики.
        /// </summary>
        /// <param name="mobileStatistics">Данные для изменениня.</param>
        /// <returns>Отображение что данные изменились.</returns>
        [HttpPut]
        public IActionResult UpdateMobileStatistics(MobileStatistics mobileStatistics)
        {
            var mobileStatisticsToUpdate = statistics.First(p => p.Id == mobileStatistics.Id);
            if(mobileStatisticsToUpdate!=null)
            {
                statistics.Remove(mobileStatisticsToUpdate);
                statistics.Add(mobileStatistics);
                return Ok();
            }
            return NotFound();
        }
    }
}
