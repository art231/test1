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
            new MobileStatistics {Id=1, Title="a123",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=2, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"},
            new MobileStatistics {Id=3, Title="a",LastStatistics=DateTime.Now, VersionClient="1",Type="windows"}};

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
        /// Мобильная статистика.
        /// </summary>
        /// <returns>Список мобильной статистики.</returns>
        [HttpGet("{id}")]
        public MobileStatistics GetById(int id)
        {
            return statistics.Find(x => x.Id == id)!;
        }
        /// <summary>
        /// Добавление статистики.
        /// </summary>
        /// <param name="mobileStatistics">новые параметры мобильной статистики.</param>
        /// <returns>true если статистика добавилась.</returns>
        [HttpPost]
        public bool Add(MobileStatistics mobileStatistics)
        {
            var maxId = statistics.MaxBy(x => x.Id);
            if (maxId == null)
            {
                mobileStatistics.Id = 1;
            }
            mobileStatistics.Id = maxId!.Id + 1;
            statistics.Add(mobileStatistics);
            return true;
        }
        /// <summary>
        /// Обновление статистики.
        /// </summary>
        /// <param name="mobileStatistics">параметры для изменения статистики.</param>
        [HttpPut]
        public void UpdateMobileStatistics(MobileStatistics mobileStatistics)
        {
            var mobileStatisticsUpdate = statistics.Where(p => p.Id == mobileStatistics.Id);

            if (mobileStatisticsUpdate.Count() > 0)
            {
                var toUpdate = mobileStatisticsUpdate.First<MobileStatistics>();

                statistics.Remove(toUpdate);
                statistics.Add(mobileStatistics);
            }
        }
    }
}
