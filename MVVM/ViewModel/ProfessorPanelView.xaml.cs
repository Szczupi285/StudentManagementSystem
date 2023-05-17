using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Logika interakcji dla klasy ProfessorPanelView.xaml
    /// </summary>
    public partial class ProfessorPanelView : Window
    {
        private Users User { get; init; }
        public ProfessorPanelView(Users user)
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

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new StudentManagementSystemContext())
            {
                int iterator = 0;
                Container.Children.Clear();
                // gets the current user professorID
                var ProfesorId = User.Professor?.Id;

                foreach(var schedule in context.Schedules.Include(c => c.Course).ThenInclude(p => p.Profesor))
                {
                    

                    if(schedule.Course.Profesor.Id == ProfesorId)
                    {

                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock containing Grade Data
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" Course Name: {schedule.Course.CourseName}\n " +
                            $"Start date: {schedule.StartDate}\n " +
                            $"End date {schedule.EndDate}";
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


            
        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {
            int iterator = 0;
            Container.Children.Clear();
            // gets the current user professorID
            var ProfesorId = User.Professor?.Id;

            using (var context = new StudentManagementSystemContext())
            {
                foreach(var course in context.Courses.Include(s => s.Students).Include(p => p.Profesor))
                {
                    if(course.Profesor.Id == ProfesorId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock and appent the coursename to it only once
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" CourseName: {course.CourseName}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;
                        GrdCrs.Padding = new Thickness(5);


                        Frame frame = new Frame();
                        frame.Content = GrdCrs;
                        frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                        frame.Margin = new Thickness(10);
                        frame.BorderThickness = new Thickness(3);
                        
                        foreach (var student in course.Students)
                        {
                            // appent studentName for every student existing in that course
                             GrdCrs.Text += $"\n Student: {student.Name} {student.Surname}";

                        }
                        // move frame to next row within the grid 
                        Grid.SetRow(frame, iterator);
                        iterator++;
                        Container.Children.Add(frame);


                    }
                }
            }



        }
    }
}
