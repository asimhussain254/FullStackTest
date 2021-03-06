using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FullStackTest.Data;

namespace FullStackTest.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        [Required]
        public Title Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public List<int> Languages { get; set; } = new List<int>();
        public List<string> LanguagesTitles { get; set; } = new List<string>();
    }
}
