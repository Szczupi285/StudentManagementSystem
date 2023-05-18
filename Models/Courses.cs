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

        public string CourseName { get; set; } = null!;

        public Professors Profesor { get; set; } = null!;

        public ICollection<StudentCourses>? StudentCourses { get; set; } 

        public ICollection<Grades>? Grades { get; set; }

    }
}
