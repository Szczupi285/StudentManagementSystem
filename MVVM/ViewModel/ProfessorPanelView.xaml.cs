using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagementSystem.MVVM.ViewModel
{
    /// <summary>
    /// Logika interakcji dla klasy ProfessorPanelView.xaml
    /// </summary>
    public partial class ProfessorPanelView : Window
    {
        private Users User { get; init; }

        private Courses? CourseId { get; set; }

        private TextBox? InputStartDate;

        private TextBox? InputEndDate;



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
        #region View Events
        private void ViewSchedule_Click(object sender, RoutedEventArgs e)
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
                            $"End date: {schedule.EndDate}";
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
       
        private void ViewGrades_Click(object sender, RoutedEventArgs e)
        {
            int iterator = 0;
            int studentCounter = 1;
            Container.Children.Clear();
            // gets the current user professorID
            var ProfesorId = User.Professor?.Id;

            using (var context = new StudentManagementSystemContext())
            {

                foreach (var course in context.Courses.Include(g => g.Grades).Include(p => p.Profesor).Include(sc => sc.StudentCourses)!.ThenInclude(s => s.Student))
                {
                    if (course.Profesor.Id == ProfesorId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock and append the coursename to it only once
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" CourseName: {course.CourseName}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;

                        // gets every student in course and adds them to textblock
                        foreach (var student in course.StudentCourses!)
                        {
                            // appent studentName for every student existing in that course
                            GrdCrs.Text += $"\n Student {studentCounter}: {student.Student.Name} {student.Student.Surname}";
                            string tempName = student.Student.Name;
                            if(course.Grades is not null)
                            {
                                GrdCrs.Text += @" {";

                                foreach (var grade in course.Grades)
                                {
                                    if(tempName == grade.Student.Name)
                                        GrdCrs.Text += $" {grade.Grade},";
                                }
                                GrdCrs.Text = GrdCrs.Text.TrimEnd(',');
                                GrdCrs.Text += " ";
                                GrdCrs.Text += @"}";
                            }
                            
                            studentCounter++;
                        }

                        studentCounter = 1;
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



        private void ViewCourses_Click(object sender, RoutedEventArgs e)
        {
            int iterator = 0;
            int studentCounter = 1;
            Container.Children.Clear();
            // gets the current user professorID
            var ProfesorId = User.Professor?.Id;

            using (var context = new StudentManagementSystemContext())
            {

                foreach(var course in context.Courses.Include(p => p.Profesor).Include(sc => sc.StudentCourses)!.ThenInclude(s => s.Student))
                {
                    if(course.Profesor.Id == ProfesorId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock and append the coursename to it only once
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" CourseName: {course.CourseName}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;

                        // gets every student in course and adds them to textblock
                        foreach (var student in course.StudentCourses!)
                        {
                            // appent studentName for every student existing in that course
                            GrdCrs.Text += $"\n Student {studentCounter}: {student.Student.Name} {student.Student.Surname}";
                            studentCounter++;
                        }

                        studentCounter = 1;
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
        #endregion

        #region Update Events



        #region Update Schedule
        private void UpdateSchedule_Click(object sender, RoutedEventArgs e)
        {
            int iterator = 0;
            Container.Children.Clear();
            // gets the current user professorID
            var ProfesorId = User.Professor?.Id;

            using (var context = new StudentManagementSystemContext())
            {

                foreach (var course in context.Courses.Include(p => p.Profesor).Include(sc => sc.StudentCourses)!.ThenInclude(s => s.Student))
                {
                    if (course.Profesor.Id == ProfesorId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock and append the coursename to it only once
                        Button CrsSch = new Button();
                        CrsSch.Tag = course.Id;
                        CrsSch.Content = $" CourseName: {course.CourseName}";
                        CrsSch.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        CrsSch.FontSize = 20;
                        CrsSch.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                        CrsSch.Click += new RoutedEventHandler(CrsSch_Click);

                        Frame frameSch = new Frame();
                        frameSch.Content = CrsSch;
                        frameSch.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                        frameSch.Margin = new Thickness(10);
                        frameSch.BorderThickness = new Thickness(3);


                        // move frame to next row within the grid 
                        Grid.SetRow(frameSch, iterator);
                        iterator++;
                        Container.Children.Add(frameSch);
                    }
                }

            }
        }

        public void CrsSch_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();

            Button temp = (Button)sender;
            var currentCourseId = temp.Tag;

            using(var context = new StudentManagementSystemContext())
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Auto);
                Container.RowDefinitions.Add(rowDef);
                StackPanel stackPanel = new StackPanel();

                TextBlock GrdCrs = new TextBlock();
                GrdCrs.Text = $"Start date: \n" +
                    $"Format: YYYY/MM/DD/hh/mm";
                GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                GrdCrs.FontSize = 20;
                GrdCrs.Padding = new Thickness(5);

                InputStartDate = new TextBox();
                InputStartDate.MaxLength = 16;
                InputStartDate.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                InputStartDate.FontSize = 20;
                InputStartDate.Width = 250;
                InputStartDate.HorizontalAlignment = HorizontalAlignment.Left;
                InputStartDate.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                InputStartDate.Padding = new Thickness(5);

                TextBlock GrdCrs2 = new TextBlock();
                GrdCrs2.Text = $"End date: \n" +
                    $"Format: YYYY/MM/DD/hh/mm";
                GrdCrs2.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                GrdCrs2.FontSize = 20;
                GrdCrs2.Padding = new Thickness(5);

                InputEndDate = new TextBox();
                InputEndDate.MaxLength = 16;
                InputEndDate.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                InputEndDate.FontSize = 20;
                InputEndDate.Width = 250;
                InputEndDate.HorizontalAlignment = HorizontalAlignment.Left;
                InputEndDate.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                InputEndDate.Padding = new Thickness(5);

                Button AddSch = new Button();
                AddSch.Tag = currentCourseId;
                AddSch.Content = $"Add new activities to schedule";
                AddSch.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                AddSch.FontSize = 20;
                AddSch.Width = 280;
                Margin = new Thickness(0,10,0,0);
                AddSch.HorizontalAlignment = HorizontalAlignment.Left;
                AddSch.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                AddSch.Click += new RoutedEventHandler(AddSch_Click);

                


                stackPanel.Children.Add(GrdCrs);
                stackPanel.Children.Add(InputStartDate);
                stackPanel.Children.Add(GrdCrs2);
                stackPanel.Children.Add(InputEndDate);
                stackPanel.Children.Add(AddSch);



                Frame frame = new Frame();
                frame.Content = stackPanel;
                frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                frame.Margin = new Thickness(10);
                frame.BorderThickness = new Thickness(3);


             
                Container.Children.Add(frame);

                


            }

        }
        // convert input to datetime
        //security methods 

        public void AddSch_Click(object sender, RoutedEventArgs e)
        {
            Button SendSchedule = (Button)sender;
            InputStartDate!.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            InputEndDate!.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));


            bool failed = false;
            var currentCourseId = SendSchedule.Tag;
            // checks if date is in proper format and is date is null
            if (InputStartDate is null || !SecurityMethods.IsValidDate(InputStartDate.Text))
            {
                InputStartDate!.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                failed = true;
            }
            if (InputEndDate is null || !SecurityMethods.IsValidDate(InputEndDate.Text))
            {
                InputEndDate!.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                failed = true;
            }
            if (failed == true)
                return;
            

                var StartTempArr = InputStartDate.Text.Split('/');
                var EndTempArr = InputEndDate.Text.Split('/');

                int[] StartDateArr = Array.ConvertAll(StartTempArr, int.Parse);
                int[] EndDateArr = Array.ConvertAll(EndTempArr, int.Parse);

                DateTime StartDate1 = new DateTime(StartDateArr[0], StartDateArr[1], StartDateArr[2],
                    StartDateArr[3], StartDateArr[4], 0);

                DateTime EndDate1 = new DateTime(EndDateArr[0], EndDateArr[1], EndDateArr[2],
                    EndDateArr[3], EndDateArr[4], 0);
                // checks if date is not from past and if StartDate > EndDate
                if(SecurityMethods.IsValidDate(StartDate1, EndDate1))
                {
                    using (var context = new StudentManagementSystemContext())
                    {
                        var Schedule_ = new Schedule
                        {
                            Course = context.Courses.Find(Convert.ToInt32(currentCourseId))!,
                            StartDate = StartDate1,
                            EndDate = EndDate1
                        };

                        context.Schedules.Add(Schedule_);
                        context.SaveChanges();
                    }
                InputStartDate.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                InputEndDate.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                InputStartDate.Text = "";
                InputEndDate.Text = "";
                return;
                }
                else
                {
                    InputStartDate.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                    InputEndDate.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                    return;
                }

                
            
        }
          

        #endregion

        #region Add Grades
        private void UpdateGrades_Click(object sender, RoutedEventArgs e)
        {
            int iterator = 0;
            Container.Children.Clear();
            // gets the current user professorID
            var ProfesorId = User.Professor?.Id;

            using (var context = new StudentManagementSystemContext())
            {

                foreach (var course in context.Courses.Include(p => p.Profesor).Include(sc => sc.StudentCourses)!.ThenInclude(s => s.Student))
                {
                    if (course.Profesor.Id == ProfesorId)
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);

                        // creates textblock and append the coursename to it only once
                        Button Crs = new Button();
                        Crs.Tag = $"{course.Id}";
                        Crs.Content = $" CourseName: {course.CourseName}";
                        Crs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        Crs.FontSize = 20;
                        Crs.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                        Crs.Click += new RoutedEventHandler(ChsCrs_Click);

                        Frame frame = new Frame();
                        frame.Content = Crs;
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


        private void ChsCrs_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();

            int iterator = 0;
            int studentCounter = 1;
            var ProfesorId = User.Professor?.Id;

            Button temp = (Button)sender;

            using (var context = new StudentManagementSystemContext())
            {

                foreach (var course in context.Courses.Include(p => p.Profesor).Include(sc => sc.StudentCourses)!.ThenInclude(s => s.Student))
                {
                    // object sender button name = id so we check if id of the clicked button = id of current course in iteration
                    // that way in if statement we only work with course that was clicked
                    if (course.Profesor.Id == ProfesorId && temp.Tag.ToString()!.Contains(course.Id.ToString()))
                    {
                        // created row definition
                        RowDefinition rowDef = new RowDefinition();
                        rowDef.Height = new GridLength(1, GridUnitType.Auto);
                        Container.RowDefinitions.Add(rowDef);



                        // displays every student in different row 
                        foreach (var student in course.StudentCourses!)
                        {
                            CourseId = course;
                            TextBlock GrdCrs = new TextBlock();
                            GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                            GrdCrs.FontSize = 20;
                            GrdCrs.Margin = new Thickness(10);

                            TextBox ChsGrd = new TextBox();
                            // sets textbox tag to current student id so we can refer later to exact student
                            ChsGrd.Tag = student.Student.Id;
                            ChsGrd.FontSize = 20;
                            ChsGrd.Width = 50;
                            ChsGrd.Height = 40;
                            ChsGrd.MaxLength = 1;
                            ChsGrd.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                            ChsGrd.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                            ChsGrd.TextChanged += new TextChangedEventHandler(ChsGrd_TextChanged);

                            Button SendGrade = new Button();
                            // associates ChsGrd with SendGrades button so we can retrive value later on
                            SendGrade.Tag = ChsGrd;
                            SendGrade.Content = "Assign grade";
                            SendGrade.Width = 300;
                            SendGrade.FontSize = 20;
                            SendGrade.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                            SendGrade.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                            SendGrade.Margin = new Thickness(10);
                            SendGrade.HorizontalAlignment = HorizontalAlignment.Right;
                            SendGrade.Click += new RoutedEventHandler(SendGrade_Click);



                            GrdCrs.Text = $" Student {studentCounter}: {student.Student.Name} {student.Student.Surname}";
                            studentCounter++;


                            Frame frame = new Frame();
                            frame.Content = GrdCrs;
                            frame.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                            frame.Margin = new Thickness(10);
                            frame.BorderThickness = new Thickness(3);


                            Frame frameChsGrd = new Frame();
                            frameChsGrd.Content = ChsGrd;
                            frameChsGrd.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
                            frameChsGrd.Margin = new Thickness(10);
                            frameChsGrd.BorderThickness = new Thickness(3);

                            // move frame to next row within the grid 
                            Grid.SetRow(frame, iterator);
                            Grid.SetRow(frameChsGrd, iterator);
                            Grid.SetRow(SendGrade, iterator);
                            iterator++;
                            Container.Children.Add(frame);
                            Container.Children.Add(frameChsGrd);
                            Container.Children.Add(SendGrade);
                        }

                        studentCounter = 1;

                    }
                }

            }
        }


        public void SendGrade_Click(object sender, RoutedEventArgs e)
        {
            Button SendGrade = (Button)sender;

            TextBox ChsGrd = (TextBox)SendGrade.Tag;
            var CurrentStudentId = ChsGrd.Tag;
            int grd = 0;
            if (ChsGrd.Text == String.Empty || ChsGrd.Text is null || !ChsGrd.Text.All(char.IsNumber))
            {
                ChsGrd.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                return;
            }
            else
                grd = Convert.ToInt32(ChsGrd.Text);

            if (CurrentStudentId is null || CourseId is null || !SecurityMethods.IsValidGrade(grd))
                return;
            else
            {
                using(var context = new StudentManagementSystemContext())
                {
                    var Grade_ = new Grades
                    {
                        // student and course use .Find method because we can't insert explicit value for indentity column so we find 
                        // the correct entity in existing db
                        Grade = grd,
                        Student = context.Students.Find(CurrentStudentId)!,
                        Course = context.Courses.Find(CourseId.Id)!

                    };
                    context.Grades.Add(Grade_);
                    context.SaveChanges();
                }   
                
            }

            ChsGrd.Text = "";
        }

        public void ChsGrd_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if(temp.Text is not null && temp.Text != String.Empty && temp.Text.All(char.IsNumber))
            {
                if (SecurityMethods.IsValidGrade(Convert.ToInt32(temp.Text.ToString())))
                {
                    temp.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                }
                else
                {
                    temp.Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                }
            }
            else
            {
                temp.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            }
           
        }
        #endregion

        private void UpdateCourses_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
