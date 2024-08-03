using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriedChickenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailService orderDetailService;
        public OrderDetailController(OrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }

        [HttpGet]
        public IEnumerable<OrderDetailDto> GetAll()
        {
            return orderDetailService.ListAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            OrderDetailDto orderDetailDto = orderDetailService.ListById(id);
            if (orderDetailDto != null) {
                return Ok(orderDetailDto);
            }
            return BadRequest("Not Found With Id is " + id);
            
        }

        [HttpPost]
        public IActionResult Add([FromBody] OrderDetailDto dto)
        {
            if (orderDetailService.Add(dto))
            {
                return Ok("Add Successfully");
            }
            else
            {
                return BadRequest("Add Failed");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody] OrderDetailDto newDto)
        {
            if (orderDetailService.Update(id, newDto))
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
            if (orderDetailService.Delete(id))
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
        public IActionResult UpdateAnyById([FromRoute] int id, [FromBody] OrderDetailDto newDto)
        {
            if (orderDetailService.UpdateAny(id, newDto))
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
