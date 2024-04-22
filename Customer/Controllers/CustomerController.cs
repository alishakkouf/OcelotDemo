using Customer.Dtos;
using Customer.Models;
using Customer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(IRepository<CustomerModel> repo) : ControllerBase
    {
        private readonly IRepository<CustomerModel> _repo = repo;


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
        public async Task<ActionResult<CustomerModel>> Create([FromBody] CreateCustomer customer)
        {
            var toBeAdded = new CustomerModel
            {
                PhoneNumber = customer.PhoneNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
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
        public async Task<IActionResult> Update(int id, UpdateCustomer product)
        {
            var data = await _repo.Get(id);

            if (data is null)
            {
                return NotFound();
            }


            data.FirstName = product.FirstName;
            data.LastName = product.LastName;
            data.PhoneNumber = product.PhoneNumber;

            await _repo.Update(data);

            return NoContent();
        }
    }
}

