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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dashboard_WPF.ViewModels;

namespace Dashboard_WPF.Views
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : UserControl
    {
        public login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            txtinfo.Text = "";
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                if (UsernameBox.Text == "123" && PasswordBox.Password == "123")
                {
                    txtinfo.Text = "";
                    mainWindow.dashboardZone.Visibility = Visibility.Visible;
                    mainWindow.profileZone.Visibility = Visibility.Visible;
                    mainWindow.DataContext = new HomeModel();
                }
                else
                {
                    txtinfo.Text = "پسورد یا نام کاربری اشتباه میباشد";
                }
            }
        }
    }
}