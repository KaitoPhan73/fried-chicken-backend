using FriedChickenStore.Model.DTOs;
using FriedChickenStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FriedChickenStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetAll()
        {
            return userService.ListAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ListById([FromRoute] int id)
        {
            UserDto userDto = userService.ListById(id);
            if (userDto != null)
            {
                return Ok(userDto);
            }
            return BadRequest("Not Found with " + id);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            ResponseLoginDto responseLoginDto = userService.Login(loginDto);

            if (responseLoginDto != null)
            {
                return Ok(responseLoginDto);
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add([FromBody] UserDto dto)
        {
            ResponseLoginDto responseLoginDto = userService.Add(dto);
            if (responseLoginDto != null)
            {
                return Ok(responseLoginDto);
            }
            else
            {
                return BadRequest("Add Failed Because UserName Or Email has existed");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, [FromBody] UserDto newDto)
        {
            if (userService.Update(id, newDto))
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
            if (userService.Delete(id))
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
        public IActionResult UpdateAnyById([FromRoute] int id, [FromBody] UserDto newDto)
        {
            if (userService.UpdateAny(id, newDto))
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
