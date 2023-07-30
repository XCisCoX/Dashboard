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
using DevExpress.Data.Helpers;
using DevExpress.Utils.CommonDialogs.Internal;
using Microsoft.Win32;

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



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SqlConnection con =
                    new SqlConnection(
                        "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
                con.Open();
                if(cbpass.IsChecked==false)
                    mainWindow.user_data["password"] = txtpass.Password;
                else 
                    mainWindow.user_data["password"] = txtpassrev.Text;
                mainWindow.user_data["phone"] = txtPhoneNumber.Text;
                mainWindow.user_data["email"] = txtemail.Text;
                
                string s = $@"update Accounts
set userAvatar = '{mainWindow.user_data["avatar"]}' , userPass='{mainWindow.user_data["password"]}',userName='{mainWindow.user_data["username"]}'
where userID= '{mainWindow.user_data["uid"]}'";

                bool success = true;

                SqlCommand sqlCommand = new SqlCommand(s, con);
                try
                {
                    SqlDataReader srd = sqlCommand.ExecuteReader();
                    srd.Close();

                }
                catch 
                {
                    success = false;
                }

                switch (mainWindow.user_data["type"])
                {
                    case "1":
                        s = $@"update Admins
set admEmail = '{mainWindow.user_data["email"]}' , admPhone='{mainWindow.user_data["phone"]}'
where adminID= '{mainWindow.user_data["uid"]}'";
                      
                        break;
                    case "2":
                        s = $@"update Students
set stuEmail = '{mainWindow.user_data["email"]}' , stuPhone='{mainWindow.user_data["phone"]}'
where studentID= '{mainWindow.user_data["uid"]}'";
                        break;
                      
                    case "3":
                          s = $@"update Teachers
set tchEmail = '{mainWindow.user_data["email"]}' , tchPhone='{mainWindow.user_data["phone"]}'
where teacherID= '{mainWindow.user_data["uid"]}'";
                        break;
                }
                sqlCommand = new SqlCommand(s, con);
                try
                {
                    SqlDataReader srd = sqlCommand.ExecuteReader();
                    srd.Close();

                }
                catch
                {
                    success = false;
                }
                if (success)
                {
                    mainWindow.imgavatar.ImageSource = new BitmapImage(new Uri(mainWindow.user_data["avatar"]));

                    MessageBox.Show("اطلاعات با موفقیت در ذخیره شد", "پیام",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("مشکلی در ذخیره سازی اطلاعات وجود دارد!", "پیام",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (cbpass.IsChecked == true)
            {
                txtpassrev.Text = txtpass.Password;
                txtpass.Visibility = Visibility.Hidden;
                txtpassrev.Visibility = Visibility.Visible;
                txtpassrev.Focus();
            }
            else
            {
                txtpass.Password = txtpassrev.Text;
                txtpass.Visibility = Visibility.Visible;
                txtpassrev.Visibility = Visibility.Hidden;
                txtpass.Focus();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                OpenFileDialog mfd = new OpenFileDialog();
                mfd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (mfd.ShowDialog() == true)
                {
                    mainWindow.user_data["avatar"] = mfd.FileName;
                    avatarimg.ImageSource=new BitmapImage(new Uri(mainWindow.user_data["avatar"]));

                }
            }
        }

        private void profileZone_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                txtpass.Password = mainWindow.user_data["password"];
                txtPhoneNumber.Text = mainWindow.user_data["phone"];
                txtfullname.Text = mainWindow.user_data["firstname"] + " " + mainWindow.user_data["lastname"];
                txtemail.Text = mainWindow.user_data["email"];
                txtUserName.Text = mainWindow.user_data["username"];
                try
                {

                   avatarimg.ImageSource = new BitmapImage(new Uri(mainWindow.user_data["avatar"]));
                }
                catch (Exception exception)
                {

                    mainWindow.imgavatar.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\images\\avatar.png"));


                }

            }
        }
    }
}
