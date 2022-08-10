using Mapster;
using Microsoft.AspNetCore.Mvc;
using MobileStatistics.Application;
using MobileStatisticsApp.Core.Entities;
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
        private readonly IUnitOfWork unitOfWork;

        private readonly ILogger<MobileStatisticsController> logger;
        /// <summary>
        /// Конструктор для логгирования.
        /// </summary>
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var statistics = await unitOfWork.MobileStatistics.GetAllAsync();
            if (statistics == null)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MobileStatisticsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var statisticsItem = await unitOfWork.MobileStatistics.GetByIdAsync(id);
            if (statisticsItem == null)
            {
                logger.LogWarning("No Mobile Statistics exist with Id {id}, returning HTTP 404 - Not Found.", id);
                return NotFound();
            }
            this.logger.LogInformation("Get by id Mobile Statistics.");
            var result = statisticsItem.Adapt<MobileStatisticsDto>();
            return Ok(result);
        }
        /// <summary>
        /// Добавление статистики.
        /// </summary>
        /// <param name="mobileStatistics">новые параметры мобильной статистики.</param>
        /// <returns>true если статистика добавилась.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> Add(MobileStatisticsItem mobileStatistics)
        {
            this.logger.LogInformation("Add new mobile statistics.");
            mobileStatistics.Id = Guid.NewGuid();
            await unitOfWork.MobileStatistics.AddAsync(mobileStatistics);
            return Ok();
        }
        /// <summary>
        /// Обновление мобильной статистики.
        /// </summary>
        /// <param name="mobileStatistics">Данные для изменениня.</param>
        /// <returns>Отображение что данные изменились.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMobileStatistics(MobileStatisticsItem mobileStatistics)
        {

            var mobileStatisticsToUpdate = await unitOfWork.MobileStatistics.GetByIdAsync(mobileStatistics.Id);
            if (mobileStatisticsToUpdate != null)
            {
                await unitOfWork.MobileStatistics.UpdateAsync(mobileStatistics);

                this.logger.LogInformation("Update mobile statistics.");
                return Ok();
            }

            logger.LogWarning("No Mobile Statistics exist with {mobileStatisticsId}, returning HTTP 404 - Not Found.", mobileStatistics.Id);
            return NotFound();
        }
    }
}
