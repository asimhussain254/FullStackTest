using System.Collections.Generic;
using System.Linq;
using FullStackTest.Data;
using FullStackTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FullStackTest.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }

        public List<UserViewModel> Read()
        {
            var userVmList = _context.User
            .Include(x => x.Languages)
            .Select(x => new UserViewModel
            {
                Id = x.Id,
                Title = x.Title,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                Phone = x.Phone,
                Gender = x.Gender,
                Languages = x.Languages.Select(x => x.LanguageId).ToList(),
                LanguagesTitles = x.Languages.Select(x => x.Language.Title).ToList()
            }).ToList();

            return userVmList;
        }

        public UserViewModel Read(int id)
        {
            var user = _context.User.Find(id);

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Title = user.Title,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
            };

            return userViewModel;
        }
        public UserViewModel Create(UserViewModel vm)
        {
            var user = new User
            {
                Title = vm.Title,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                DateOfBirth = vm.DateOfBirth,
                Email = vm.Email,
                Phone = vm.Phone,
                Gender = vm.Gender,
            };

            _context.User.Add(user);
            _context.SaveChanges();

            var languages = vm.Languages.Select(language => new UserLanguage
            {
                LanguageId = (int)language,
                UserId = user.Id
            }).ToList();

            _context.UserLanguages.AddRange(languages);

            _context.SaveChanges();

            var userVm = Read(user.Id);

            return userVm;
        }

        public UserViewModel Update(int id, UserViewModel userVm)
        {
            var user = _context.User.Find(id);
            var languages = _context.UserLanguages.Where(x => x.UserId == user.Id);

            if (user == null)
            {
                throw new System.Exception("User does not exist");
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
            _context.SaveChanges();
            // Make list of Languages user select now
            var userlanguages = userVm.Languages.Select(language => new UserLanguage
            {
                LanguageId = (int)language,
                UserId = user.Id
            }).ToList();
            // Add Languages to UserLanguages Entity
            _context.UserLanguages.AddRange(userlanguages);
            _context.SaveChanges();

            _context.Entry(user).State = EntityState.Modified;

            var updatedUser = Read(id);
            return updatedUser;
        }

        public bool Delete(int id)
        {
            var user = _context.User.Find(id);
            // var languages = _context.UserLanguage.Where(i => i.UserId == user.Id);
            var languages = _context.UserLanguages.Where(i => i.UserId == user.Id);

            if (user == null)
            {
                throw new System.Exception("User not found");
            }

            _context.User.Remove(user);
            _context.UserLanguages.RemoveRange(languages);
            var result = _context.SaveChanges();

            return result > 0;
        }


    }
}