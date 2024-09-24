using Store.Data.Entities;
using Store.Service.services.product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.services.product
{
    public interface IProductService

    {

        Task<productDetailsDto> GetProductByIdAsync(int? id);
        Task<IReadOnlyList<productDetailsDto>> GetAllProductsAsync();
        Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductBrandAsync();
        Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductTypeAsync();
    }





}

