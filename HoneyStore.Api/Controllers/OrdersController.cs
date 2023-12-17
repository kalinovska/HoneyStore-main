using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyStore.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper, ICartItemService cartItemService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            if (orders == null)
            {
                return NoContent();
            }

            return Ok(orders);
        }


        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                if (orders == null)
                {
                    return NoContent();
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var order = _mapper.Map<OrderDto>(model);
                await _orderService.AddOrderAsync(order);

                foreach (var cartItemId in model.CartItemIds)
                {
                    var cartItem = await _cartItemService.GetCartItemAsync(cartItemId);

                    if (cartItem == null) continue;
                    
                    cartItem.OrderId = order.Id;
                    await _cartItemService.UpdateCartItemAsync(cartItemId, cartItem);
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto order)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var orderFromDb = await _orderService.GetOrderAsync(id);
                if (orderFromDb == null)
                {
                    await _orderService.AddOrderAsync(order);

                    return Ok(order);
                }

                await _orderService.UpdateOrderAsync(id, order);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var orderFromDb = await _orderService.GetOrderAsync(id);

                if (orderFromDb == null)
                {
                    return NotFound();
                }

                await _orderService.RemoveOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
