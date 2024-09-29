
using Store.Data.Entities;
namespace Store.Repository.Specification

{

    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(int? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> GetCountSpecificationAsync(ISpecification<TEntity> specification);    //count pagination     
        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);

    }
}
