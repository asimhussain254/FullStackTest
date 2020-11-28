using System.Collections.Generic;
using FullStackTest.Data;
using FullStackTest.ViewModels;

namespace FullStackTest.Services
{
    // Contract
    public interface IUserService
    {
        // CRUD
        List<UserViewModel> Read();
        UserViewModel Read(int id);
        UserViewModel Create(UserViewModel vm);
        UserViewModel Update(int id, UserViewModel vm);
        bool Delete(int id);
    }
}