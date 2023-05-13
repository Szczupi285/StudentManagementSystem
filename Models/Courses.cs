using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Courses
    {
        public int Id { get; set; }

        public Professors Profesor { get; set; } = null!;

        public ICollection<Students> Students { get; set; } = null!;
    }
}
