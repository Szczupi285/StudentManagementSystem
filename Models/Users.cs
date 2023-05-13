﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsProfesor { get; set; }

        public Students? Student { get; set; }

        public Professors? Professor { get; set; }
    }
}
