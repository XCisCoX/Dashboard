using Dashboard_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Dashboard_WPF.Views;
using System.Windows.Threading;

namespace Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string,string> user_data=new Dictionary<string,string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void home_clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HomeModel();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            txtDate.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

        }

    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dashboardZone.Visibility = Visibility.Hidden;
            profileZone.Visibility = Visibility.Hidden;
            DataContext = new login();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0,500);
            dispatcherTimer.Start();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new courses();
        }

        private void btn_teacher_send_message(object sender, RoutedEventArgs e)
        {
            DataContext = new contact();
        }

        private void btm_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

   
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dashboardZone.Visibility = Visibility.Hidden;
            profileZone.Visibility = Visibility.Hidden;
            DataContext = new login();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = new edit_user();
            dashboardZone.Visibility = Visibility.Hidden;
            profileZone.Visibility = Visibility.Hidden;
        }

        private void shahrieitem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new student_shahrie();
        }
    }
}

