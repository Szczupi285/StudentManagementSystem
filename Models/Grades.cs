using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Grades
    {
        public int Id { get; set; }

        public Students Student { get; set; } = null!;

        public Professors Professor { get; set; } = null!;



    }
}
