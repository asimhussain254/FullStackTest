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
        public int userID { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string lastName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string firstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string dateOfBirth { get; set; }
        [Column(TypeName = "date")]
        public string email { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string phone { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string gender { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string phone { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string language { get; set; }
    }
}
