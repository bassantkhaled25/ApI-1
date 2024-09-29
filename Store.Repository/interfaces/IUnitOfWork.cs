using Store.Data.Entities;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IUnitOfWork                                                 //register in program to inject it in(...)
    {                                                                            //ex.TEntity(productBrand) , TKey => id(int)
        public IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;  //function 1 مسئوله تجيبلي object => من الحاجه اللي هطلبها
        Task<int> CompeleteAsync();                                              //func 2 (saving)                   
    }  

