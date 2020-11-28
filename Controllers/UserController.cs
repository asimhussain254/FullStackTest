using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FullStackTest.ViewModels;
using FullStackTest.Services;

namespace FullStackTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<UserViewModel>> GetUser()
        {
            var users = _userService.Read();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUser([FromRoute] int id)
        {

            var user = _userService.Read(id);

            return Ok(User);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<UserViewModel> PostUser([FromBody] UserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _userService.Create(userVm);

            return Ok(newUser);

        }


        // PUT: api/User/5
        [HttpPut("{id}")]
        public ActionResult<UserViewModel> PutUser([FromRoute] int id, [FromBody] UserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = _userService.Update(id, userVm);

            return Ok(updatedUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteUser([FromRoute] int id)
        {

            var success = _userService.Delete(id);

            return Ok(success);
        }

    }
}