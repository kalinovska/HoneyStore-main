using HoneyStore.BusinessLogic.Models;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetCommentAsync(int id);

        Task<ICollection<CommentDto>> GetAllCommentsAsync();

        Task<ICollection<CommentDto>> GetCommentsByProductIdAsync(int productId);

        Task AddCommentAsync(CommentDto comment);

        Task RemoveCommentAsync(int id);

        Task UpdateCommentAsync(int id, CommentDto comment);
    }
}