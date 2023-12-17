using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryAsync(int id);

        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();

        Task AddCategoryAsync(CategoryDto category);

        Task RemoveCategoryAsync(int id);

        Task UpdateCategoryAsync(int id, CategoryDto category);
    }
}