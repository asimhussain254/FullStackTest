using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackTest.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DateOfBirth { get; set; }
        [Column(TypeName = "date")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Language { get; set; }
    }
}
