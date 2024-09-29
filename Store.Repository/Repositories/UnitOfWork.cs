using Store.Data.Entities;
using Store.Repository.Specification;
using System.Collections;
using Store.Data.contexts;


    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositories;                                //hash table(key,value)


                                                                       //1- inject dbcontext
        public UnitOfWork(StoreDbContext context)

        {
            _context = context;
        }


        public async Task<int> CompeleteAsync()

         => await _context.SaveChangesAsync();

     


                                                                              //2- implement function to get object from repsitories
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositories == null)                
                _repositories = new Hashtable(); 
          
            var entitytype = typeof(TEntity).Name;                               // the entity name as string"" 

            if (!_repositories.ContainsKey(entitytype))

            {
                var reopsitoryType = typeof(GenericRepository<,>);

                var repositoryInstanse = Activator.CreateInstance(reopsitoryType.MakeGenericType(typeof(TEntity),typeof(TKey)), _context);  //مسئوله تطلع instance

                _repositories.Add(entitytype, repositoryInstanse);
            }
                                                         
            return (IGenericRepository<TEntity,TKey>)_repositories[entitytype];     //casting to (IGeneric rep)                                         
        }



    }

