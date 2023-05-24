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

        private TextBox? CrsNameInput;

        private TextBox? StudEmailInput;



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

        #region UpdateCourses
        private void UpdateCourses_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            


            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.VerticalAlignment = VerticalAlignment.Center;


            Button AddCrs = new Button();
            AddCrs.Content = $"Add Course";
            AddCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
            AddCrs.FontSize = 20;
            AddCrs.Height = 200;
            AddCrs.Width = 400;
            AddCrs.Margin = new Thickness(30);
            AddCrs.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            AddCrs.Click += new RoutedEventHandler(AddCrsView_Click);

            Button AddStud = new Button();
            AddStud.Content = $"Add students to course";
            AddStud.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
            AddStud.FontSize = 20;
            AddStud.Height = 200;
            AddStud.Width = 400;
            AddStud.Margin = new Thickness(30);
            AddStud.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            AddStud.Click += new RoutedEventHandler(AddStudView_Click);


            Frame frameCrs = new Frame();
            frameCrs.Content = AddStud;
            frameCrs.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
            frameCrs.Margin = new Thickness(10);
            frameCrs.BorderThickness = new Thickness(3);

            Frame frameStud = new Frame();
            frameStud.Content = AddCrs;
            frameStud.BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112));
            frameStud.Margin = new Thickness(10);
            frameStud.BorderThickness = new Thickness(3);

            stackPanel.Children.Add(frameCrs);
            stackPanel.Children.Add(frameStud);

            Container.Children.Add(stackPanel);
        }


       
        public void AddStudView_Click(object sender, RoutedEventArgs e)
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

                        StackPanel stackPanel = new StackPanel();



                        // creates textblock and append the coursename to it only once
                        TextBlock GrdCrs = new TextBlock();
                        GrdCrs.Text = $" CourseName: {course.CourseName}";
                        GrdCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        GrdCrs.FontSize = 20;

                        TextBlock StudEmail = new TextBlock();
                        StudEmail.Text = $"Student E-Mail";
                        StudEmail.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        StudEmail.FontSize = 20;
                        StudEmail.Width = 200;
                        StudEmail.Height = 50;
                        StudEmail.HorizontalAlignment = HorizontalAlignment.Left;
                        StudEmail.Padding = new Thickness(5);

                        StudEmailInput = new TextBox();
                        StudEmailInput.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        StudEmailInput.Tag = course.Id;
                        StudEmailInput.FontSize = 20;
                        StudEmailInput.Height = 60;
                        StudEmailInput.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                        StudEmailInput.Width = 300;
                        StudEmailInput.HorizontalAlignment = HorizontalAlignment.Left;
                        StudEmailInput.Padding = new Thickness(5);


                        Button AddStud = new Button();
                        AddStud.Content = $"Add Student";
                        AddStud.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
                        AddStud.FontSize = 20;
                        AddStud.Height = 60;
                        AddStud.Width = 300;
                        AddStud.HorizontalAlignment = HorizontalAlignment.Left;
                        AddStud.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
                        AddStud.Click += new RoutedEventHandler(AddStud_Click);

                        stackPanel.Children.Add(GrdCrs);
                        stackPanel.Children.Add(StudEmail);
                        stackPanel.Children.Add(StudEmailInput);
                        stackPanel.Children.Add(AddStud);

                        Frame frame = new Frame();
                        frame.Content = stackPanel;
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

        // to do validation checker and expception
        public void AddStud_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new StudentManagementSystemContext())
            {

                // gets the current user professorID
                var ProfesorId = User.Professor?.Id;
                var s = StudEmailInput.Text;
                var Stud = context.Students.FirstOrDefault(Students => Students.Email == StudEmailInput.Text);
                if (StudEmailInput is null || StudEmailInput.Text is null || StudEmailInput.Text == String.Empty)
                    return;
                else
                {
                    var StudCrs = new StudentCourses
                    {
                        Course = context.Courses.Find(Convert.ToInt32(StudEmailInput.Tag)),
                        Student = context.Students.Find(Convert.ToInt32(Stud.Id))!
                    };

                    StudEmailInput.Text = "";
                    context.StudentCourses.Add(StudCrs);
                    context.SaveChanges();
                }

            }
        }



        public void AddCrsView_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            
            

            TextBlock CrsName = new TextBlock();
            CrsName.Text = $"Course Name";
            CrsName.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
            CrsName.FontSize = 20;
            CrsName.Width = 200;
            CrsName.Height = 200;
            CrsName.Margin = new Thickness(30);
            CrsName.HorizontalAlignment = HorizontalAlignment.Left;
            CrsName.Padding = new Thickness(5);

            CrsNameInput = new TextBox();
            CrsNameInput.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
            CrsNameInput.FontSize = 20;
            CrsNameInput.Height = 60;
            CrsNameInput.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            CrsNameInput.Width = 200;
            CrsNameInput.HorizontalAlignment = HorizontalAlignment.Left;
            CrsNameInput.Padding = new Thickness(5);


            Button AddCrs = new Button();
            AddCrs.Content = $"Create New Course";
            AddCrs.Foreground = new SolidColorBrush(Color.FromRgb(154, 209, 212));
            AddCrs.FontSize = 20;
            AddCrs.Height = 150;
            AddCrs.Width = 300;
            AddCrs.HorizontalAlignment = HorizontalAlignment.Center;
            AddCrs.Margin = new Thickness(30);
            AddCrs.Background = new SolidColorBrush(Color.FromRgb(20, 33, 61));
            AddCrs.Click += new RoutedEventHandler(AddCrs_Click);

            Container.Children.Add(CrsName);
            Container.Children.Add(CrsNameInput);
            Container.Children.Add(AddCrs);

        }

        public void AddCrs_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new StudentManagementSystemContext())
            {

                // gets the current user professorID
                var ProfesorId = User.Professor?.Id;

                if (CrsNameInput is null || CrsNameInput.Text is null || CrsNameInput.Text == String.Empty)
                    return;
                else
                {
                    var Course_ = new Courses
                    {
                        CourseName = CrsNameInput.Text,
                        Profesor = context.Professors.Find(ProfesorId)!
                    };
                    CrsNameInput.Text = "";
                    context.Courses.Add(Course_);
                    context.SaveChanges();
                }
               
            }
        }



        #endregion

        #endregion
    }
}
