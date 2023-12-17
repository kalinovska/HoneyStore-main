using HoneyStore.DataAccess.Entities;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        double GetMarkByProductId(int productId);

        Task<ICollection<Comment>> GetCommentsByProductIdAsync(int productId);
    }
}