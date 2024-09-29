
using Store.Data.Entities;
using Store.Repository.specification.productSpecs;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class ProductWithCountSpecification : BaseSpecification<Product>

    {
       public ProductWithCountSpecification(productspecification specs)         //GetAllProducts  بشتغل ع list          //chaining

         : base (Product => (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId.Value) &&
                            (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId.Value)&&
             (string.IsNullOrEmpty(specs.Search) || Product.Name.ToLower().Trim().Contains(specs.Search)))
       {
        
       }
    }
}
