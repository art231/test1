using MobileStatisticsApp.Application.Services;
using MobileStatisticsApp.Core.Entities;
using MobileStatisticsApp.Repositories;
using System.Data;

namespace MobileStatisticsApp.Infrastructure.Services
{
    /// <summary>
    /// Сервис.
    /// </summary>
    public class MobileStatisticsService : IMobileStatisticsService
    {
        private IMobileStatisticsRepository mobileStatisticsRepository;
        private IDbConnection dbConnection;
        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="mobileStatisticsRepository">Репозитарий.</param>
        public MobileStatisticsService(IMobileStatisticsRepository mobileStatisticsRepository,
            IDbConnection dbConnection)
        {
            this.mobileStatisticsRepository = mobileStatisticsRepository;
            this.dbConnection = dbConnection;
        }
        /// <summary>
        /// Получение всех узлов.
        /// </summary>
        /// <returns>Количество.</returns>
        public async Task<IReadOnlyList<MobileStatisticsItem>> GetAllAsync()
        {
            IReadOnlyList<MobileStatisticsItem> result;
            using DalSession dalSession = new DalSession(dbConnection);
            UnitOfWork unitOfWork = dalSession.UnitOfWork;

            unitOfWork.Begin();
            try
            {
                result = await this.mobileStatisticsRepository.GetAllAsync();

                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
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
            using DalSession dalSession = new DalSession(dbConnection);
            UnitOfWork unitOfWork = dalSession.UnitOfWork;

            unitOfWork.Begin();
            try
            {
                result = await this.mobileStatisticsRepository.GetByIdAsync(id);

                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
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
            using DalSession dalSession = new DalSession(dbConnection);
            UnitOfWork unitOfWork = dalSession.UnitOfWork;

            unitOfWork.Begin();
            try
            {
                await this.mobileStatisticsRepository.AddAsync(entity);

                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
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
            using DalSession dalSession = new DalSession(dbConnection);
            UnitOfWork unitOfWork = dalSession.UnitOfWork;

            unitOfWork.Begin();
            try
            {
                await this.mobileStatisticsRepository.UpdateAsync(entity);

                unitOfWork.Commit();
            }
            catch
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}
