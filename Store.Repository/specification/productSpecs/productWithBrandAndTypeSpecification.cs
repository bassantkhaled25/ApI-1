using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Entities;
using Store.Repository.Specification;

namespace Store.Repository.specification.productSpecs
{
    public class productWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public productWithBrandAndTypeSpecification(productspecification specs) :         //GetAllProducts  بشتغل ع list          //chaining

          base(Product => (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId.Value) &&                 //سواء فيه او لأ 
                         (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId.Value) &&
                         (string.IsNullOrEmpty(specs.Search) || Product.Name.ToLower().Trim().Contains(specs.Search)))
        {

            AddInClude(x => x.Brand);                   //types and brands specification
            AddInClude(x => x.Type);
            AddOrderBy(x => x.Name);
            ApplyPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);  //skip - take


            if (!string.IsNullOrEmpty(specs.Sort))

            {
                switch (specs.Sort)

                {
                    case "PriceAsc":
                        AddOrderBy(x => x.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public productWithBrandAndTypeSpecification(int? Id) : base (x => x.Id == Id)    //GetById   (not list)     
            

        {             
                AddInClude(x => x.Brand);
                AddInClude(x => x.Type);
        }


        }
    }

