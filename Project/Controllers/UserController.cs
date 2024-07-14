using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DAL.Interfaces;
using DAL.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface DB_User;

        public UserController(IUserInterface DbUser)
        {
            DB_User = DbUser;
        }

        //[HttpGet]
        //public async Task<IEnumerable<Property>> Get()
        //{
        //    return await _userService.GetAllPropertiesAsync();
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO id)
        {
           bool create=await DB_User.AddUser(id);
            if (create)
                return Ok();
            return BadRequest();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Property property)
        //{
        //    await _userService.AddPropertyAsync(property);
        //    return CreatedAtAction(nameof(Get), new { id = property.Id }, property);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, Property property)
        //{
        //    var existingProperty = await _userService.GetPropertyByIdAsync(id);
        //    if (existingProperty == null)
        //    {
        //        return NotFound();
        //    }
        //    await _userService.UpdatePropertyAsync(id, property);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var existingProperty = await _userService.GetPropertyByIdAsync(id);
        //    if (existingProperty == null)
        //    {
        //        return NotFound();
        //    }
        //    await _userService.DeletePropertyAsync(id);
        //    return NoContent();
        //}

        //[HttpPost("{propertyId}/comments")]
        //public async Task<IActionResult> AddComment(string propertyId, Comment comment)
        //{
        //    await _userService.AddCommentAsync(propertyId, comment);
        //    return NoContent();
        //}
    }
}
