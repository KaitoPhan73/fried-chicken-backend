using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriedChickenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductController(ProductService productService)
        {
           this.productService = productService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<ProductDto> GetAll()
        {
            return productService.ListAll();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IActionResult ListById([FromRoute] int id)
        {
            ProductDto productDto = productService.ListById(id);
            if (productDto != null) 
            {
                return Ok(productDto);
            }
            return BadRequest("Not Found with " + id);
            
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductDto dto)
        {
            if (productService.Add(dto))
            {
                return Ok("Add Successfully");
            }
            else
            {
                return BadRequest("Add Failed");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody] ProductDto newDto)
        {
            if (productService.Update(id, newDto))
            {
                return Ok("Update Successfully");
            }
            else
            {
                return BadRequest("Update Failed");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            if (productService.Delete(id))
            {
                return Ok("Delete Successfully");
            }
            else
            {
                return BadRequest("Delete Failed");
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateAnyById([FromRoute] int id, [FromBody] ProductDto newDto)
        {
            if (productService.UpdateAny(id, newDto))
            {
                return Ok("Update Successfully");
            }
            else
            {
                return BadRequest("Update Failed");
            }
        }
    }
}
