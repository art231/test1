using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;

namespace MobileStatisticsApp.Infrastructure.Services
{
    /// <summary>
    /// Сервис.
    /// </summary>
    public class MobileStatisticsService : IMobileStatisticsService
    {
        private IMobileStatisticsRepository mobileStatisticsRepository;
        private IDalSession dalSession;
        private UnitOfWork unitOfWork;
        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="mobileStatisticsRepository">Репозитарий.</param>
        /// <param name="dalSession">Сессия.</param>
        public MobileStatisticsService(IMobileStatisticsRepository mobileStatisticsRepository,
            IDalSession dalSession)
        {
            this.mobileStatisticsRepository = mobileStatisticsRepository;
            this.dalSession= dalSession;
            this.unitOfWork= this.dalSession.UnitOfWork;
        }
        /// <summary>
        /// Получение всех узлов.
        /// </summary>
        /// <returns>Количество.</returns>
        public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
        {
            IReadOnlyList<MobileStatisticsItem> result;
            this.unitOfWork.Begin();
            try
            {
                result = await this.mobileStatisticsRepository.GetAllAsync();

                this.unitOfWork.Commit();
            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
            return result;
        }
        /// <summary>
        /// Получение по айди.
        /// </summary>
        /// <param name="id">Айди.</param>
        /// <returns>Узел.</returns>
        public async Task<MobileStatisticsItem> GetByIdAsync(Guid id)
        {
            MobileStatisticsItem result;
            this.unitOfWork.Begin();
            try
            {
                result = await this.mobileStatisticsRepository.GetByIdAsync(id);

                this.unitOfWork.Commit();
            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
            return result;
        }
        /// <summary>
        /// Добавление новой сущности.
        /// </summary>
        /// <param name="entity">Новая сущность.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task AddAsync(MobileStatisticsItem entity)
        {
            this.unitOfWork.Begin();
            try
            {
                await this.mobileStatisticsRepository.AddAsync(entity);

                this.unitOfWork.Commit();
            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
        }
        /// <summary>
        /// Обновление объекта.
        /// </summary>
        /// <param name="entity">Объект для изменения.</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateAsync(MobileStatisticsItem entity)
        {
            this.unitOfWork.Begin();
            try
            {
                await this.mobileStatisticsRepository.UpdateAsync(entity);

                this.unitOfWork.Commit();
            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
        }
    }
}
