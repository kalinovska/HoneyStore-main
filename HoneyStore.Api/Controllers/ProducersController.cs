using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyStore.Api.Controllers
{
    [Route("api/producers")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            var producers = await _producerService.GetAllProducersAsync();

            if (producers == null)
            {
                return NoContent();
            }

            return Ok(producers);
        }


        [HttpGet("{id}", Name = "ProducerById")]
        public async Task<IActionResult> GetProducer(int id)
        {
            var producer = await _producerService.GetProducerAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return Ok(producer);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProducer([FromBody] ProducerDto producer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _producerService.AddProducerAsync(producer);

                return CreatedAtRoute("CategoryById", new { id = producer.Id }, producer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducer(int id, [FromBody] ProducerDto producer)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var producerDto = await _producerService.GetProducerAsync(id);

                if (producerDto == null)
                {
                    return NotFound();
                }

                await _producerService.UpdateProducerAsync(id, producer);
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryFromDb = await _producerService.GetProducerAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            await _producerService.RemoveProducerAsync(id);
            return Ok();
        }
    }
}