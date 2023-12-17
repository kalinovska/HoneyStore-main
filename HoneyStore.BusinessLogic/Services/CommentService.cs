using AutoMapper;
using HoneyStore.BusinessLogic.Helpers;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapperFactory factory) : base(unitOfWork)
        {
            _mapper = factory.CreateMapper();
        }

        public async Task<ICollection<CommentDto>> GetAllCommentsAsync()
        {
            var commentEntities =  await _uow.Comments.GetAllAsync();

            var commentDtos = _mapper.Map<ICollection<Comment>, ICollection<CommentDto>>(commentEntities);

            return commentDtos;
        }

        public async Task<CommentDto> GetCommentAsync(int id)
        {
            var commentEntity = await _uow.Comments.GetAsync(id);

            var commentDto = _mapper.Map<Comment,CommentDto>(commentEntity);

            return commentDto;
        }

        public async Task<ICollection<CommentDto>> GetCommentsByProductIdAsync(int productId)
        {
            var commentEntities = await _uow.Comments.GetCommentsByProductIdAsync(productId);

            var commentDtos = _mapper.Map<ICollection<Comment>, ICollection<CommentDto>>(commentEntities);

            return commentDtos;
        }

        public async Task AddCommentAsync(CommentDto comment)
        {
            var commentEntity = _mapper.Map<CommentDto, Comment>(comment);

            await _uow.Comments.AddAsync(commentEntity);

            await _uow.SaveAsync();
            comment.Id = commentEntity.Id;
        }

        public async Task RemoveCommentAsync(int id)
        {
            var comment = await _uow.Comments.GetAsync(id);

            await _uow.Comments.RemoveAsync(comment);

            await _uow.SaveAsync();
        }

        public async Task UpdateCommentAsync(int id, CommentDto comment)
        {
            var commentEntity = _mapper.Map<CommentDto, Comment>(comment);

            await _uow.Comments.UpdateAsync(id, commentEntity);

            await _uow.SaveAsync();
        }
    }
}