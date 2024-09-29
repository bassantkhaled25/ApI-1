using Microsoft.EntityFrameworkCore;
using Store.Data.contexts;
using Store.Data.Entities;
using Store.Repository.Specification;


namespace Store.Repository.Specification
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)                   //inject dbcontext

        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(int? id)
            => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
           => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specification)
           => await ApplySpecification(specification).ToListAsync();

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specification)
           => await ApplySpecification(specification).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)        //method مجمعه اللي عاوزه
           => SpecificationEvaluator<TEntity,TKey>.GetQuery(_context.Set<TEntity>(), specification);

        public async Task<int> GetCountSpecificationAsync(ISpecification<TEntity> specification)
         => await ApplySpecification(specification).CountAsync();
    }
}
