﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentManagementSystem.Models
{
    public class Grades
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public Students Student { get; set; } = null!; 

        public Courses Course { get; set; } = null!;
    }
}
