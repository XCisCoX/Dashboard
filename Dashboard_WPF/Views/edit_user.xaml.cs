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

namespace Dashboard_WPF.Views
{
    /// <summary>
    /// Interaction logic for edit_user.xaml
    /// </summary>
    public partial class edit_user : UserControl
    {
        public edit_user()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.profileZone.Visibility = Visibility.Visible;
                mainWindow.dashboardZone.Visibility = Visibility.Visible;
                mainWindow.DataContext = new Home();
            }
        }
    }
}
