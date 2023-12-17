using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Repositories;
using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private IProductPhotoRepository _productPhotoRepository;
        private IProducerRepository _producersRepository;
        private ICategoryRepository _categoryRepository;
        private ICommentRepository _commentRepository;
        private IWishRepository _wishRepository;
        private IOrderRepository _orderRepository;
        private ICartItemRepository _cartItemRepository;

        private readonly StoreDbContext _context;
        private bool disposed;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            disposed = false;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public IProductPhotoRepository Photos => _productPhotoRepository ??= new ProductPhotoRepository(_context);

        public IProducerRepository Producers => _producersRepository ??= new ProducerRepository(_context);

        public ICommentRepository Comments => _commentRepository ??= new CommentRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);

        public IWishRepository Wishes => _wishRepository ??= new WishRepository(_context);

        public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

        public ICartItemRepository CartItems => _cartItemRepository ??= new CartItemRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
