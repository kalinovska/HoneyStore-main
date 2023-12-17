using System;
using System.Threading.Tasks;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IProductPhotoRepository Photos { get; }
        IProducerRepository Producers { get; }
        ICommentRepository Comments { get; }
        ICategoryRepository Categories { get; }
        IWishRepository Wishes { get; }
        IOrderRepository Orders { get; }
        ICartItemRepository CartItems { get; }
        Task<int> SaveAsync();
    }
}
