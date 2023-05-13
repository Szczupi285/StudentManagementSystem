using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Professors
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Title { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

          

    }
}
