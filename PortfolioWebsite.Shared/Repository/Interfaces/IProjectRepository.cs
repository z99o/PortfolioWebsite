using MongoDB.Driver;
namespace PortfolioWebsite.Shared.Repository
{
    public interface IProjectRepository<T, K>
    {
        Task<List<T>> GetAll();
        Task<T> GetByID(K id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(K ID);
    }
}
