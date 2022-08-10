namespace MobileStatisticsApp.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}