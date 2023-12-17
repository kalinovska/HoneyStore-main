using AutoMapper;
using HoneyStore.BusinessLogic.Helpers;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class CategoryService: BaseService, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapperFactory factory) : base(uow)
        {
            _mapper = factory.CreateMapper();
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var categoryEntity = await _uow.Categories.GetAsync(id);

            var categoryDto = _mapper.Map<Category, CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var categoryEntities = await _uow.Categories.GetAllAsync();

            var categoryDtos = _mapper.Map<ICollection<Category>, ICollection<CategoryDto>>(categoryEntities);

            return categoryDtos;
        }
        
        public async Task AddCategoryAsync(CategoryDto category)
        {
            var categoryEntity = _mapper.Map<CategoryDto, Category>(category);

            await _uow.Categories.AddAsync(categoryEntity);

            await _uow.SaveAsync();
            category.Id = categoryEntity.Id;
        }

        public async Task RemoveCategoryAsync(int id)
        {
            var categoryEntity = await _uow.Categories.GetAsync(id);

            await _uow.Categories.RemoveAsync(categoryEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateCategoryAsync(int id, CategoryDto category)
        {
            var categoryEntity = _mapper.Map<CategoryDto, Category>(category);

            await _uow.Categories.UpdateAsync(id, categoryEntity);

            await _uow.SaveAsync();
        }
    }
}
