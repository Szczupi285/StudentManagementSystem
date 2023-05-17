﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class TableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CoursesId",
                table: "Grades",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CoursesId",
                table: "Grades",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CoursesId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CoursesId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Grades");
        }
    }
}