using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FullStackTest.ViewModels;
using FullStackTest.Services;

namespace FullStackTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult<List<LanguageViewModel>> GetLanguage()
        {
            var languageVmList = _languageService.Read();

            return Ok(languageVmList);
        }
        [HttpGet("{id}")]
        public ActionResult<LanguageViewModel> GetLanguage([FromRoute] int id)
        {
            var languageVmList = _languageService.Read(id);

            return Ok(languageVmList);
        }
        [HttpPost]
        public ActionResult<LanguageViewModel> PostLanguage([FromBody] LanguageViewModel languageVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newLanguage = _languageService.Create(languageVm);

            return Ok(newLanguage);

        }
        [HttpPut("{id}")]
        public ActionResult<LanguageViewModel> PutLanguage([FromRoute] int id, [FromBody] LanguageViewModel languageVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedLanguage = _languageService.Update(id, languageVm);

            return Ok(updatedLanguage);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLanguage4([FromRoute] int id)
        {

            var success = _languageService.Delete(id);

            return Ok(success);
        }
    }
}