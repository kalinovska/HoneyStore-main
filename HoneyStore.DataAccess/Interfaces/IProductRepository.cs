using HoneyStore.DataAccess.Entities;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<ICollection<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task<ICollection<Product>> GetProductsByNameAsync(string name);
    }
}