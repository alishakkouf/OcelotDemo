using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Dtos;
using Order.Models;
using Order.Repository;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IRepository<OrderModel> repo) : ControllerBase
    {
        private readonly IRepository<OrderModel> _repo = repo;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var data = await _repo.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var data = await _repo.Get(id);

            if (data is null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Create([FromBody] CreateOrder order)
        {
            var toBeAdded = new OrderModel
            {
                Number = order.Number,
                Date = order.Date,
            };

            var data = await _repo.Create(toBeAdded);

            return CreatedAtAction(nameof(Get), new { Id = data.Id }, data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _repo.Get(id);

            if (data is null)
            {
                return NotFound();
            }

            await _repo.Delete(data);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrder order)
        {
            var data = await _repo.Get(id);

            if (data is null)
            {
                return NotFound();
            }


            data.Number = order.Number;
            data.Date = order.Date;

            await _repo.Update(data);

            return NoContent();
        }
    }
}
