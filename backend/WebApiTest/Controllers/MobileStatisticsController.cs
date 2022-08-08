using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatisticsApp.Dtos;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            if(statistics==null)
            {
                return NotFound();
            }
            this.logger.LogInformation("Get data.");
            var result = statistics.Adapt<List<MobileStatisticsDto>>();
            return Ok(result);
        }
        /// <summary>
        /// возвращает статистику отдельного устройства.
        /// </summary>
        /// <param name="id">Уникальный ключ.</param>
        /// <returns>Мобильную статистику устройства.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var resultFromRepo = statistics.FirstOrDefault(x => x.Id == id);
            if(resultFromRepo == null)
            {
                logger.LogWarning("No Mobile Statistics exist with Id {id}, returning HTTP 404 - Not Found.", id);
                return NotFound();
            }
            this.logger.LogInformation("Get by id Mobile Statistics.");
            resultFromRepo.Adapt<MobileStatisticsDto>();
            return Ok(resultFromRepo);
        }
        /// <summary>
        /// Добавление статистики.
        /// </summary>
        /// <param name="mobileStatistics">новые параметры мобильной статистики.</param>
        /// <returns>true если статистика добавилась.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Add(MobileStatistics mobileStatistics)
        {
            this.logger.LogInformation("Add new mobile statistics.");
            mobileStatistics.Id = Guid.NewGuid();
            statistics.Add(mobileStatistics);
            return Ok();
        }
        /// <summary>
        /// Обновление мобильной статистики.
        /// </summary>
        /// <param name="mobileStatistics">Данные для изменениня.</param>
        /// <returns>Отображение что данные изменились.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMobileStatistics(MobileStatistics mobileStatistics)
        {
            
            var mobileStatisticsToUpdate = statistics.First(p => p.Id == mobileStatistics.Id);
            if(mobileStatisticsToUpdate!=null)
            {
                statistics.Remove(mobileStatisticsToUpdate);
                statistics.Add(mobileStatistics);

                this.logger.LogInformation("Update mobile statistics.");
                return Ok();
            }

            logger.LogWarning("No Mobile Statistics exist with {mobileStatisticsId}, returning HTTP 404 - Not Found.", mobileStatistics.Id);
            return NotFound();
        }
    }
}
