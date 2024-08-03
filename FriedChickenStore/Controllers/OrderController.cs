using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FriedChickenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<OrderDto> GetAll()
        {
            return orderService.ListAll();
        }

        [HttpGet("status/{orderStatus}")]
        public IEnumerable<OrderDto> GetAllByOrderStatus(String orderStatus)
        {
            return orderService.ListAllByOrderStatus(orderStatus);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            OrderDto orderDto = orderService.ListById(id);
            if(orderDto != null) {
                return Ok(orderDto);
            }
            return BadRequest("Not Found with Id is "+ id);
        }

        [HttpPost]
        public IActionResult Add([FromBody] OrderDto dto)
        {
            if (orderService.Add(dto))
            {
                return Ok("Add Successfully");
            }
            else
            {
                return BadRequest("Add Failed");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody] OrderDto newDto)
        {
            if (orderService.Update(id, newDto))
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
            if (orderService.Delete(id))
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
        public IActionResult UpdateAnyById([FromRoute] int id, [FromBody] OrderDto newDto)
        {
            if (orderService.UpdateAny(id, newDto))
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
