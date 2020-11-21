using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackTest.Data
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(5)] //enum
        public Title Title { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(10)]
        public Gender Gender { get; set; }
        public List<UserLanguage> Languages { get; set; } = new List<UserLanguage>();
    }
}
