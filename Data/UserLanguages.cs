using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackTest.Data
{
    public class UserLanguages
    {
        public int Id { get; set; }

        [MaxLength(5)] //enum
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
