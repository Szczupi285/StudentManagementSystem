using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.MVVM.Model;
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

namespace StudentManagementSystem.MVVM.ViewModel
{
    /// <summary>
    /// Logika interakcji dla klasy LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            this.Close();
            registerView.Show();
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPopup.Visibility = Visibility.Hidden;

            if (Login.Text == String.Empty && Password.Password == String.Empty)
            {
                LoginPopup.Text = "Enter Your login and password";
                LoginPopup.Visibility = Visibility.Visible;
            }
            else
            {
                using (var context = new StudentManagementSystemContext())
                {
                    var HashedPassword = SecurityMethods.HashSha256(Password.Password);
                    var user = context.Users.Include(x => x.Professor).Include(x => x.Student).FirstOrDefault(Users => Users.Login == Login.Text && Users.Password == HashedPassword);



                    if (user == null)
                    {
                            LoginPopup.Text = "Wrong login or password";
                            LoginPopup.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (user.IsProfesor) 
                        {
                            ProfessorPanelView professorPanelView = new ProfessorPanelView(user);
                            this.Close();
                            professorPanelView.Show();
                        }
                        else
                        {
                            StudentPanelView studentPanelView = new StudentPanelView(user);
                            this.Close();
                            studentPanelView.Show();
                        }
                    }

                }
            }

            
        }
    }
}
