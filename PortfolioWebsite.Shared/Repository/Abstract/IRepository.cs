

using MongoDB.Driver;
namespace PortfolioWebsite.Shared.Repository.Abstract
{
    public interface IRepository<T, K>
    {
        IMongoCollection<T> Records { get;}
        Task<List<T>> GetAll();
        Task<T> GetByID(K id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<K> Delete(K id);
    }
}
