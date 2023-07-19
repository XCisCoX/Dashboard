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

namespace Dashboard_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            /*   SqlConnection con = new SqlConnection("Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
               con.Open();
               SqlCommand userName = new SqlCommand("select CONCAT(STName,' ',STLastName) from Students where stuID=1",con);
               SqlDataReader srd= userName.ExecuteReader();
               while(srd.Read())
               {
                   txtUserName.Text=srd.GetValue(0).ToString();
               }
               con.Close();*/
            dashboardZone.Visibility = Visibility.Hidden;
            profileZone.Visibility = Visibility.Hidden;
            DataContext = new login();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new courses();
        }

        private void btn_teacher_send_message(object sender, RoutedEventArgs e)
        {
            DataContext = new contact();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dashboardZone.Visibility = Visibility.Hidden;
            profileZone.Visibility = Visibility.Hidden;
            DataContext = new login();
        }
    }
}

