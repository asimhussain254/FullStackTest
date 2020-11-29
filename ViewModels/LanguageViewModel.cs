using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FullStackTest.Data;

namespace FullStackTest.ViewModels
{
    public class LanguageViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
