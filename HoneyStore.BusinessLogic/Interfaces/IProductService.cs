using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(int id);

        Task<ICollection<ProductDto>> GetAllProductsAsync();

        Task<ICollection<ProductDto>> GetProductsByNameAsync(string name);

        Task AddProductAsync(ProductDto product, ProductPhotoDto photo);

        Task RemoveProductAsync(int id);

        Task UpdateProductAsync(int id, ProductDto product);

        Task<ICollection<ProductDto>> GetProductsByCategoryId(int categoryId);
    }
}