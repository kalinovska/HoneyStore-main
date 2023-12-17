using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Repositories
{
    public class ProductPhotoRepository : BaseRepository<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(StoreDbContext context) : base(context)
        {
        }
    }
}
