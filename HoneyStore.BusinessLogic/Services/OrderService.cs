using AutoMapper;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.BusinessLogic.Services
{
    public class OrderService: BaseService, IOrderService
    {
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var orderEntity = await _uow.Orders.GetAsync(id);

            var orderDto = _mapper.Map<Order, OrderDto>(orderEntity);

            return orderDto;
        }

        public async Task<ICollection<OrderDto>> GetAllOrdersAsync()
        {
            var orderEntities = await _uow.Orders.GetAllAsync();

            var orderDtos = _mapper.Map<IEnumerable<Order>, ICollection<OrderDto>>(orderEntities);

            return orderDtos;
        }

        public async Task<ICollection<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orderEntities = await _uow.Orders.GetOrdersByUserId(userId);

            var orderDtos = _mapper.Map<IEnumerable<Order>, ICollection<OrderDto>>(orderEntities);

            return orderDtos;
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            var orderEntity = _mapper.Map<OrderDto, Order>(order);

            await _uow.Orders.AddAsync(orderEntity);

            await _uow.SaveAsync();
            order.Id = orderEntity.Id;
        }

        public async Task RemoveOrderAsync(int id)
        {
            var orderEntity = await _uow.Orders.GetAsync(id);

            await _uow.Orders.RemoveAsync(orderEntity);

            await _uow.SaveAsync();
        }

        public async Task UpdateOrderAsync(int id, OrderDto order)
        {
            var orderEntity = _mapper.Map<OrderDto, Order>(order);

            await _uow.Orders.UpdateAsync(id, orderEntity);

            await _uow.SaveAsync();
        }
    }
}
