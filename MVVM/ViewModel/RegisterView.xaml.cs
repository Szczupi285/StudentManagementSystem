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

        private void ProfessorLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidLogin(ProfessorLogin.Text) && ProfessorLogin.Text.Length > 0)
                ProfessorLoginPopup.Visibility = Visibility.Visible;
            else
                ProfessorLoginPopup.Visibility = Visibility.Hidden;
        }

        private void ProfessorPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            Regex rx = new Regex(@"[!@#$%^&*()_+\-=\[\]{};:,.<>\?]");
            if (!SecurityMethods.IsValidPassword(ProfessorPassword.Password) && ProfessorPassword.Password.Length > 0)
            {
                StringBuilder s = new StringBuilder();
                if (ProfessorPassword.Password.Length > 7 && ProfessorPassword.Password.Length < 32)
                    s.AppendLine("Password Lenght must be higher than 7 and lower than 33 \u2713");
                else
                    s.AppendLine("Password Lenght must be higher than 7 and lower than 33\u2717");
                if (ProfessorPassword.Password.Any(char.IsNumber) && rx.IsMatch(ProfessorPassword.Password))
                    s.AppendLine("Password Must contain Number and special character\u2713");
                else
                    s.AppendLine("Password Must contain Number and special character \u2717");
                if (ProfessorPassword.Password.Any(char.IsUpper) && ProfessorPassword.Password.Any(char.IsUpper))
                {
                    s.AppendLine("Password must contain Uppercase and Lowercase letter\u2713");
                }
                else
                    s.AppendLine("Password must contain Uppercase and Lowercase letter\u2717");

                ProfessorPasswordPopup.Visibility = Visibility.Visible;
                ProfessorPasswordPopup.Text = s.ToString();
            }
            else
            {
                ProfessorPasswordPopup.Visibility = Visibility.Hidden;
            }

        }

        private void ProfessorEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidEmail(ProfessorEmail.Text) && ProfessorEmail.Text.Length > 0)
                ProfessorEmailPopup.Visibility = Visibility.Visible;
            else
                ProfessorEmailPopup.Visibility = Visibility.Hidden;

        }
        

      

        private void ProfessorPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SecurityMethods.IsValidPhoneNumber(ProfessorPhoneNumber.Text) && ProfessorPhoneNumber.Text.Length > 0)
                ProfessorPhoneNumberPopup.Visibility = Visibility.Visible;
            else
                ProfessorPhoneNumberPopup.Visibility = Visibility.Hidden;
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

        // since combo box style can't be changed without template modification we use textbox to represent what has been picked
        private void TitleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem != null)
            {
                string selectedContent = selectedItem.Content.ToString()!;

                if (selectedContent == "Professor")
                {
                    ComboBoxPick.Text = "Professor";
                }
                else if (selectedContent == "Doctor")
                {
                    ComboBoxPick.Text = "Doctor";
                }
                else if (selectedContent == "Master")
                {
                    ComboBoxPick.Text = "Master";
                }
                else if (selectedContent == "Bachelor")
                {
                    ComboBoxPick.Text = "Bachelor";
                }
                else if (selectedContent == "Dean")
                {
                    ComboBoxPick.Text = "Dean";
                }
                else if (selectedContent == "None")
                {
                    ComboBoxPick.Text = "None";
                }
            }
        }


        private void StudentRegister_Click(object sender, RoutedEventArgs e)
        {
            FailedStudentRegisterPopup.Visibility = Visibility.Hidden;
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
                        FailedStudentRegisterPopup.Text = "You have Registered Succesfully";
                        FailedStudentRegisterPopup.Visibility = Visibility.Visible;
                        FailedStudentRegisterPopup.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else if (DoesPhoneNumberExist != null)
                    {
                        FailedStudentRegisterPopup.Text = "Phone Number is already taken";
                        FailedStudentRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if(DoesEmailExist != null)
                    {
                        FailedStudentRegisterPopup.Text = "Email is already taken";
                        FailedStudentRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if(DoesLoginExist != null)
                    {
                        FailedStudentRegisterPopup.Text = "Login is already taken";
                        FailedStudentRegisterPopup.Visibility = Visibility.Visible;
                    }

                }


            }
        }

        public void ProfesorRegister_Click(object sender, RoutedEventArgs e)
        {
            FailedProfessorRegisterPopup.Visibility = Visibility.Hidden;
            using (var context = new StudentManagementSystemContext())
            {
                if (ProfessorLogin.Text is not null && ProfessorPassword.Password is not null
                    && ProfessorName.Text is not null && ProfessorSurname.Text is not null)
                {

                    string[] data = SecurityMethods.TrimAll(ProfessorLogin.Text, ProfessorPassword.Password, ProfessorName.Text,
                        ProfessorSurname.Text, ProfessorEmail.Text, ProfessorPhoneNumber.Text);

                    string HashedPassword = SecurityMethods.HashSha256(data[1]);

                    // checks if data already exsit in database
                    var DoesPhoneNumberExist = context.Professors.FirstOrDefault(Professors => Professors.PhoneNumber == data[5]);
                    var DoesEmailExist = context.Professors.FirstOrDefault(Professors => Professors.Email == data[4]);
                    var DoesLoginExist = context.Users.FirstOrDefault(Professors => Professors.Login == data[0]);
                    // checks what title has been picked
                    string? title;
                    if (ComboBoxPick.Text == "None" || ComboBoxPick.Text == "Choose title")
                        title = null;
                    else
                    {
                        title = ComboBoxPick.Text;
                    }



                    // validation methods
                    if (SecurityMethods.IsValidLogin(data[0]) && SecurityMethods.IsValidPassword(data[1])
                        && SecurityMethods.IsValidEmail(data[4]) && SecurityMethods.IsValidPhoneNumber(data[5])
                        && DoesPhoneNumberExist == null && DoesEmailExist == null && DoesLoginExist == null)
                    {

                        var Professor = new Professors
                        {
                            Name = data[2],
                            Surname = data[3],
                            Title = title,
                            Email = data[4],
                            PhoneNumber = data[5],

                        };
                        context.Professors.Add(Professor);

                        var User = new Users
                        {
                            Login = data[0],
                            Password = HashedPassword,
                            IsProfesor = true,
                            Professor = Professor,

                        };
                        context.Users.Add(User);

                        context.SaveChanges();



                        // clear the textboxes after user click register button and student is registered
                        ProfessorLogin.Text = String.Empty;
                        ProfessorPassword.Password = String.Empty;
                        ProfessorName.Text = String.Empty;
                        ProfessorSurname.Text = String.Empty;
                        ComboBoxPick.Text = "Choose title";
                        ProfessorEmail.Text = String.Empty;
                        ProfessorPhoneNumber.Text = String.Empty;
                        FailedProfessorRegisterPopup.Text = "You have Registered Succesfully";
                        FailedProfessorRegisterPopup.Visibility = Visibility.Visible;
                        FailedProfessorRegisterPopup.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else if (DoesPhoneNumberExist != null)
                    {
                        FailedProfessorRegisterPopup.Text = "Phone Number is already taken";
                        FailedProfessorRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if (DoesEmailExist != null)
                    {
                        FailedProfessorRegisterPopup.Text = "Email is already taken";
                        FailedProfessorRegisterPopup.Visibility = Visibility.Visible;
                    }
                    else if (DoesLoginExist != null)
                    {
                        FailedProfessorRegisterPopup.Text = "Login is already taken";
                        FailedProfessorRegisterPopup.Visibility = Visibility.Visible;
                    }

                }


            }
        }
    }
}
