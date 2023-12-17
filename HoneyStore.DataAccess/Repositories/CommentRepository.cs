using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(StoreDbContext context) : base(context)
        {

        }

        public override async Task<Comment> GetAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstAsync(c=>c.Id==id);
        }

        public override async Task<ICollection<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(c => c.Product)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<ICollection<Comment>> GetCommentsByProductIdAsync(int productId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.ProductId == productId)
                .Select(c => c).ToListAsync();
        }

        public double GetMarkByProductId(int productId)
        {
            var comments = _context.Comments
                .Where(c => c.ProductId == productId)
                .Select(c => c.Mark);

            if (comments.Any())
            {
                return comments.Average();
            }

            return 0;
        }

        public override async Task UpdateAsync(int id, Comment comment)
        {
            var commentFromDb = await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);

            commentFromDb.Mark = comment.Mark;
            commentFromDb.Content = comment.Content;

            _context.Comments.Update(commentFromDb);
        }
    }
}