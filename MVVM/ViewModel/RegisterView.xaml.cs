using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
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
using StudentManagementSystem.MVVM.Model;
using System.Text.RegularExpressions;

namespace StudentManagementSystem.MVVM.ViewModel
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
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
        
        private void GoToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            this.Close();
            loginView.Show();
        }

        #region real time validation 
        private void StudentLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidLogin(StudentLogin.Text) && StudentLogin.Text.Length > 0)
                StudentLoginPopup.Visibility = Visibility.Visible;
            else
                StudentLoginPopup.Visibility = Visibility.Hidden;
        }
        private void StudentPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Regex rx = new Regex(@"[!@#$%^&*()_+\-=\[\]{};:,.<>\?]");
            if (!SecurityMethods.IsValidPassword(StudentPassword.Password) && StudentPassword.Password.Length > 0)
            {
                StringBuilder s = new StringBuilder();
                if (StudentPassword.Password.Length > 7 && StudentPassword.Password.Length < 32)
                    s.AppendLine("Password Lenght must be higher than 7 and lower than 33 \u2713");
                else
                    s.AppendLine("Password Lenght must be higher than 7 and lower than 33\u2717");
                if (StudentPassword.Password.Any(char.IsNumber) && rx.IsMatch(StudentPassword.Password))
                    s.AppendLine("Password Must contain Number and special character\u2713");
                else
                    s.AppendLine("Password Must contain Number and special character \u2717");
                if (StudentPassword.Password.Any(char.IsUpper) && StudentPassword.Password.Any(char.IsUpper))
                {
                    s.AppendLine("Password must contain Uppercase and Lowercase letter\u2713");
                }
                else
                    s.AppendLine("Password must contain Uppercase and Lowercase letter\u2717");

                StudentPasswordPopup.Visibility = Visibility.Visible;
                StudentPasswordPopup.Text = s.ToString();
            }
            else
            {
                StudentPasswordPopup.Visibility = Visibility.Hidden;
            }

        }

        private void StudentEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidEmail(StudentEmail.Text) && StudentEmail.Text.Length > 0)
                StudentEmailPopup.Visibility = Visibility.Visible;
            else
                StudentEmailPopup.Visibility = Visibility.Hidden;

        }

        private void StudentPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidPhoneNumber(StudentPhoneNumber.Text) && StudentPhoneNumber.Text.Length > 0)
                StudentPhoneNumberPopup.Visibility = Visibility.Visible;
            else
                StudentPhoneNumberPopup.Visibility = Visibility.Hidden;
            
        }

        #endregion
        // makes it unpossible to type a letter in PhoneNumber TextBox
        private void StudentPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void ProfessorPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }


        private void StudentRegister_Click(object sender, RoutedEventArgs e)
        {
            FailedRegisterPopup.Visibility = Visibility.Hidden;
            using (var context = new StudentManagementSystemContext())
            {
                if (StudentLogin.Text is not null && StudentPassword.Password is not null
                    && StudentName.Text is not null && StudentSurname.Text is not null)
                {
                    
                    string[] data = SecurityMethods.TrimAll(StudentLogin.Text, StudentPassword.Password, StudentName.Text,
                        StudentSurname.Text, StudentEmail.Text, StudentPhoneNumber.Text);

                    string HashedPassword = SecurityMethods.HashSha256(data[1]);

                    // checks if data already exsit in database
                    var DoesPhoneNumberExist = context.Students.FirstOrDefault(Students => Students.PhoneNumber == data[5]);
                    var DoesEmailExist = context.Students.FirstOrDefault(Students => Students.Email == data[4]);
                    var DoesLoginExist = context.Users.FirstOrDefault(Users => Users.Login == data[0]);



                    // validation methods
                    if (SecurityMethods.IsValidLogin(data[0]) && SecurityMethods.IsValidPassword(data[1])
                        && SecurityMethods.IsValidEmail(data[4]) && SecurityMethods.IsValidPhoneNumber(data[5])
                        && DoesPhoneNumberExist == null && DoesEmailExist == null && DoesLoginExist == null)
                    {
                        var Student = new Students
                        {
                            Name = data[2],
                            Surname = data[3],
                            Email = data[4],
                            PhoneNumber = data[5],

                        };
                        context.Students.Add(Student);

                        var User = new Users
                        {
                            Login = data[0],
                            Password = HashedPassword,
                            IsProfesor = false,
                            Student = Student,

                        };
                        context.Users.Add(User);

                        context.SaveChanges();



                        // clear the textboxes after user click register button and student is registered
                        StudentLogin.Text = String.Empty;
                        StudentPassword.Password = String.Empty;
                        StudentName.Text = String.Empty;
                        StudentSurname.Text = String.Empty;
                        StudentEmail.Text = String.Empty;
                        StudentPhoneNumber.Text = String.Empty;
                        FailedRegisterPopup.Text = "You have Registered Succesfully";
                        FailedRegisterPopup.Visibility = Visibility.Visible;
                        FailedRegisterPopup.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else if (DoesPhoneNumberExist != null)
                    {
                        FailedRegisterPopup.Text = "Phone Number is already taken";
                        FailedRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if(DoesEmailExist != null)
                    {
                        FailedRegisterPopup.Text = "Email is already taken";
                        FailedRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if(DoesLoginExist != null)
                    {
                        FailedRegisterPopup.Text = "Login is already taken";
                        FailedRegisterPopup.Visibility = Visibility.Visible;
                    }





                }


            }
        }
    }
}
