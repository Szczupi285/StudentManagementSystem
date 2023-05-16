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

        }

        private void Grades_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Courses_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
