using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullStackTest.Models;

namespace FullStackTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = await _context.User.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] UserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _context.User.Find(id);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            user.Title = userVm.Title;
            user.FirstName = userVm.FirstName;
            user.LastName = userVm.LastName;
            user.DateOfBirth = userVm.DateOfBirth;
            user.Email = userVm.Email;
            user.Phone = userVm.Phone;
            user.Gender = userVm.Gender;
            user.Language = userVm.Language;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User();
            user.Title = userVm.Title;
            user.FirstName = userVm.FirstName;
            user.LastName = userVm.LastName;
            user.DateOfBirth = userVm.DateOfBirth;
            user.Email = userVm.Email;
            user.Phone = userVm.Phone;
            user.Gender = userVm.Gender;
            user.Language = userVm.Language;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.User.Remove(User);
            await _context.SaveChangesAsync();

            return Ok(User);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }
    }
}