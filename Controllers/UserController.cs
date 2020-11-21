using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullStackTest.Data;
using FullStackTest.ViewModels;

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
        [HttpGet]
        [Route("api/Language")]
        public IEnumerable<Language> GetLanguage()
        {
            return _context.Language;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserViewModel> GetUser()
        {

            var userVmList = _context.User.Include(x => x.Languages).Select(x => new UserViewModel
            {
                id = x.Id,
                Title = x.Title,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                Phone = x.Phone,
                Gender = x.Gender,
                Languages = x.Languages.Select(x => x.LanguageId).ToList()
            }).ToList();
            return userVmList;
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
            var languages = _context.UserLanguages.Where(x => x.UserId == user.Id);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            //Update data in User Entity
            user.Title = userVm.Title;
            user.FirstName = userVm.FirstName;
            user.LastName = userVm.LastName;
            user.DateOfBirth = userVm.DateOfBirth;
            user.Email = userVm.Email;
            user.Phone = userVm.Phone;
            user.Gender = userVm.Gender;
            // remove current languages of users
            _context.UserLanguages.RemoveRange(languages);
            await _context.SaveChangesAsync();
            // Make list of Languages user select now
            var userlanguages = userVm.Languages.Select(language => new UserLanguage
            {
                LanguageId = (int)language,
                UserId = user.Id
            }).ToList();
            // Add Languages to UserLanguages Entity
            await _context.UserLanguages.AddRangeAsync(userlanguages);
            await _context.SaveChangesAsync();

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

            var user = new User
            {
                Title = userVm.Title,
                FirstName = userVm.FirstName,
                LastName = userVm.LastName,
                DateOfBirth = userVm.DateOfBirth,
                Email = userVm.Email,
                Phone = userVm.Phone,
                Gender = userVm.Gender,
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            var languages = userVm.Languages.Select(language => new UserLanguage
            {
                LanguageId = (int)language,
                UserId = user.Id
            }).ToList();

            await _context.UserLanguages.AddRangeAsync(languages);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new
            {
                id = user.Id
            }, user);
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
            return _context.User.Any(e => e.Id == id);
        }
    }
}