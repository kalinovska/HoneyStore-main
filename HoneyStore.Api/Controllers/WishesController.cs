using AutoMapper;
using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyStore.Api.Controllers
{
    [Route("api/wishes")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IWishService _wishService;
        private readonly IMapper _mapper;

        public WishesController(IWishService wishService, IMapper mapper)
        {
            _wishService = wishService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishes()
        {
            var wishes = await _wishService.GetAllWishesAsync();

            if (wishes == null)
            {
                return NoContent();
            }

            return Ok(wishes);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWishesByUserId(int userId)
        {
            var wishes = await _wishService.GetWishesByUserIdAsync(userId);

            if (wishes == null)
            {
                return NoContent();
            }

            return Ok(wishes);
        }

        [HttpPost]
        public async Task<IActionResult> AddWish([FromBody] WishCreationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var wish = _mapper.Map<WishDto>(model);
                await _wishService.AddWishAsync(wish);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("product/{productId}/user/{userId}")]
        public async Task<IActionResult> DeleteWish([FromRoute] int productId, [FromRoute] int userId)
        {
            var wishFromDb = await _wishService.GetWishAsync(userId, productId);

            if (wishFromDb == null)
            {
                return NotFound();
            }

            await _wishService.RemoveWishAsync(userId, productId);
            return Ok();
        }
    }
}