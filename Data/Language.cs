using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackTest.Data
{
    public class Language
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<UserLanguage> Languages { get; set; } = new List<UserLanguage>();

    }
}

