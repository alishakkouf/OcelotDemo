using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Dtos;
using Product.Models;
using Product.Repository;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IRepository<ProductModel> repo) : ControllerBase
    {
        private readonly IRepository<ProductModel> _repo = repo;


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
        public async Task<ActionResult<ProductModel>> Create([FromBody]CreateProduct product)
        {
            var toBeAdded = new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            var data = await _repo.Create(toBeAdded);

            return CreatedAtAction(nameof(Get),new {Id = data.Id}, data);
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
        public async Task<IActionResult> Update(int id, UpdateProduct product)
        {
            var data = await _repo.Get(id);

            if (data is null)
            {
                return NotFound();
            }


            data.Name = product.Name;
            data.Description = product.Description;
            data.Price = product.Price;            

             await _repo.Update(data);

            return NoContent();
        }
    }
}
