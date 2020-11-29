using System.Collections.Generic;
using FullStackTest.Data;
using FullStackTest.ViewModels;

namespace FullStackTest.Services
{
    // Contract
    public interface ILanguageService
    {
        // CRUD
        List<LanguageViewModel> Read();
        LanguageViewModel Read(int id);
        LanguageViewModel Create(LanguageViewModel vm);
        LanguageViewModel Update(int id, LanguageViewModel vm);
        bool Delete(int id);
    }
}