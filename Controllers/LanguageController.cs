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
    public class LanguageController : ControllerBase
    {
        private readonly UserContext _context;
        public LanguageController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<LanguageViewModel> GetLanguage()
        {
            var languageVmList = _context.Language.Select(x => new LanguageViewModel
            {
                id = x.Id,
                Title = x.Title,
            });

            return languageVmList;
        }
    }
}