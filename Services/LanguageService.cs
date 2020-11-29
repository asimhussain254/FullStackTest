using System.Collections.Generic;
using System.Linq;
using FullStackTest.Data;
using FullStackTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FullStackTest.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly UserContext _context;

        public LanguageService(UserContext context)
        {
            _context = context;
        }

        public List<LanguageViewModel> Read()
        {
            var userVmList = _context.Language
            .Include(x => x.Languages)
            .Select(x => new LanguageViewModel
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();

            return userVmList;
        }

        public LanguageViewModel Read(int id)
        {
            var language = _context.Language.Find(id);

            var languageViewModel = new LanguageViewModel
            {
                Id = language.Id,
                Title = language.Title
            };

            return languageViewModel;
        }
        public LanguageViewModel Create(LanguageViewModel vm)
        {
            var language = new Language
            {
                Title = vm.Title
            };

            _context.Language.Add(language);
            _context.SaveChanges();
            var languageVm = Read(language.Id);

            return languageVm;
        }

        public LanguageViewModel Update(int id, LanguageViewModel languageVm)
        {
            var language = _context.Language.Find(id);

            if (language == null)
            {
                throw new System.Exception("User does not exist");
            }

            language.Title = languageVm.Title;
            _context.SaveChanges();
            var updatedUser = Read(id);
            return updatedUser;
        }

        public bool Delete(int id)
        {
            var language = _context.Language.Find(id);

            if (language == null)
            {
                throw new System.Exception("User not found");
            }

            _context.Language.Remove(language);
            var result = _context.SaveChanges();

            return result > 0;
        }


    }
}