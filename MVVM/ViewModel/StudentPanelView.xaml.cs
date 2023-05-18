using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentManagementSystem.MVVM.ViewModel
{
    /// <summary>
    /// Logika interakcji dla klasy StudentPanelView.xaml
    /// </summary>
    public partial class StudentPanelView : Window
    {
        private Users User { get; init; }
        public StudentPanelView(Users user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void Minimalize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Fullscreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #region ShowStudentData

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StudentManagementSystemContext())
            {
                Container.Children.Clear();

                int? UserStudId = User.Student?.Id;
                int iterator = 0;

                
                HashSet<int> courseId = new HashSet<int>();
               
                foreach(var sc in context.StudentCourses.Include(s => s.Student).Include(s => s.Course))
                {
                    if(sc.Student.Id == UserStudId)
                    {
                        courseId.Add(sc.Course.Id);                          
                    }
                }

                foreach (var schedule in context.Schedules.Include(c => c.Course))
                {
                    if (courseId.Contains(schedule.Course.Id))
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock containing Grade Data
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" Course Name: {schedule.Course.CourseName} \n" +
                            $" Start Date: {schedule.StartDate}\n End Date: {schedule.EndDate}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;
                        GrdCrs.Padding = new Thickness(5);


                        Frame frame = new Frame();
                        frame.Content = GrdCrs;
                        frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                        frame.Margin = new Thickness(10);
                        frame.BorderThickness = new Thickness(3);

                        // move frame to next row within the grid 
                        Grid.SetRow(frame, iterator);
                        iterator++;
                        Container.Children.Add(frame);

                    }
                }

            }
        }

        
        private void Grades_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StudentManagementSystemContext())
            {
                Container.Children.Clear();

                int? UserStudId = User.Student?.Id;

                int iterator = 0;

                foreach (var grade in context.Grades.Include(s => s.Student).Include(c => c.Course).ThenInclude(p => p.Profesor))
                {
                    if (grade.Student?.Id == UserStudId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock containing Grade Data
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" Course Name: {grade.Course.CourseName}\n Grade: {grade.Grade}\n Given by: {grade.Course.Profesor.Name}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;
                        GrdCrs.Padding = new Thickness(5);

                       
                        Frame frame = new Frame();
                        frame.Content = GrdCrs;
                        frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                        frame.Margin = new Thickness(10);
                        frame.BorderThickness = new Thickness(3);
                        

                        // move frame to next row within the grid 
                        Grid.SetRow(frame, iterator);
                        iterator++;
                        Container.Children.Add(frame);
                    }

                }
               
            }
        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StudentManagementSystemContext())
            {
                Container.Children.Clear();

                int? UserStudId = User.Student?.Id;

             /*   var Courses = context.Courses.Include(s => s.Students).Include(p => p.Profesor);
                int iterator = 0;


                foreach (var course in Courses)
                {
                    foreach(var student in course.Students)
                    {
                        if(student.Id == UserStudId)
                        {

                            // created row definition
                            RowDefinition rowDef = new RowDefinition();
                            rowDef.Height = new GridLength(1, GridUnitType.Auto);
                            Container.RowDefinitions.Add(rowDef);

                            // creates textblock containing Grade Data
                            TextBlock GrdCrs = new TextBlock();
                            GrdCrs.Text = $" Course name: {course.CourseName}\n Professor: {course.Profesor.Name}";
                            GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                            GrdCrs.FontSize = 20;
                            GrdCrs.Padding = new Thickness(5);


                            Frame frame = new Frame();
                            frame.Content = GrdCrs;
                            frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                            frame.Margin = new Thickness(10);
                            frame.BorderThickness = new Thickness(3);


                            // move frame to next row within the grid 
                            Grid.SetRow(frame, iterator);
                            iterator++;
                            Container.Children.Add(frame);

                        }
                    }
                }
             */
            }
        }

        #endregion
    }
}
