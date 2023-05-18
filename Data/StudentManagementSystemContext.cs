using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data
{
    public class StudentManagementSystemContext : DbContext
    {
        public DbSet<Professors> Professors { get; set; } = null!;
        public DbSet<Students> Students { get; set; } = null!;
        public DbSet<Grades> Grades { get; set; } = null!;
        public DbSet<Courses> Courses { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;  
        public DbSet<StudentCourses> StudentCourses { get; set; } = null!;  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["StudentManagementSystemDatabase"].ConnectionString);
        }


    }
}
