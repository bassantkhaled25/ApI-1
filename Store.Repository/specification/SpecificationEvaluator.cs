using Store.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification

{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>   //base entity => عشان بس يشتغل ع الحاجه اللي عاوزها
                                                                                                 //حلقه وصل بين ال rep , specification (Evaluate الحاجه اللي جيالي)
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)       //method

        {
            var query = inputQuery;                                                  //IQuarable

            if (specification.Criteria != null)

            {
                query = query.Where(specification.Criteria);                      //x => x.typeId = 3              //Criteria and includes(in ISpecification)
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);                     //x => x.Name
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPaginated)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;

        }
    }
}
