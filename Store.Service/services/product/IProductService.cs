using Store.Repository.specification.productSpecs;
using Store.Service.Helper;
using Store.Service.services.product.Dtos;

namespace Store.Service.services.product
{
    public interface IProductService

    {

        Task<productDetailsDto> GetProductByIdAsync(int? id);
        Task<Pagination<productDetailsDto>> GetAllProductsAsync(productspecification input);
        Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductBrandAsync();
        Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductTypeAsync();
    }





}

