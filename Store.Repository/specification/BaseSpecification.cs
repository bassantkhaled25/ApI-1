using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>                 //implement interface
    {
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)                     //constructor for criteria

        {
            Criteria = criteriaExpression;                                                         //initialize
        }

        public Expression<Func<T, bool>> Criteria { get ; }

        public List<Expression<Func<T, object>>> Includes { get ; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {  get; private set; }

        public Expression<Func<T, object>> OrderByDescending {  get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }



        protected void AddInClude(Expression<Func<T, object>> IncludeExpression)      //methodos for includes

          => Includes.Add(IncludeExpression);


        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)

          => OrderBy = OrderByExpression;


        protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)

          => OrderByDescending = OrderByDescendingExpression;

        protected void ApplyPagination(int skip, int take)

        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }

    }
}

