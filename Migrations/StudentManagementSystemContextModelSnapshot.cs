﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagementSystem.Data;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    [DbContext(typeof(StudentManagementSystemContext))]
    partial class StudentManagementSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentManagementSystem.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Grades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Professors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.StudentCourses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Students", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsProfesor")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Courses", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Professors", "Profesor")
                        .WithMany("Courses")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Grades", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Courses", "Course")
                        .WithMany("Grades")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentManagementSystem.Models.Students", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Schedule", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Courses", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.StudentCourses", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Courses", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentManagementSystem.Models.Students", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Users", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Professors", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");

                    b.HasOne("StudentManagementSystem.Models.Students", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Professor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Courses", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Professors", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Students", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
