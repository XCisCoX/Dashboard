using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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
                string s = $@"SELECT 
[userId]      
,[userName]
      ,[userPass]
      ,[accTyp]
      ,[userAvatar]
  FROM [CollegeProject].[dbo].[Accounts] where userName='{UsernameBox.Text}' and userPass='{PasswordBox.Password}'
";
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(s, con);
                SqlDataReader srd = sqlCommand.ExecuteReader();
                string AccTy="", userInfo="",stuId="";
                bool isLogin = false;
                while (srd.Read())
                {
                    AccTy = srd.GetValue(3).ToString();
                    isLogin=true;
                    stuId = srd.GetValue(0).ToString();
                  
                }
                srd.Close();
                SqlCommand uinfCommand;

                if (isLogin)
                {
                    txtinfo.Text = "";
                    switch (AccTy)
                    {
                        case "2":
                            userInfo = $@"SELECT 
       CONCAT([firstName]
      ,' ',[lastName])
  FROM [CollegeProject].[dbo].[Students] where studentID={stuId}
";

                            uinfCommand = new SqlCommand(userInfo, con);
                            srd = uinfCommand.ExecuteReader();
                            while (srd.Read())
                            {
                                mainWindow.txtfullname.Text = srd.GetValue(0).ToString();

                                mainWindow.txtLevel.Text = "دانشجو";
                            }
                            srd.Close();
                            break;
                        case "3":
                            userInfo = $@"SELECT 
       CONCAT([firstName]
      ,' ',[lastName])
  FROM [CollegeProject].[dbo].[Teachers] where teacherID={stuId}
";


                            uinfCommand = new SqlCommand(userInfo, con);
                            srd = uinfCommand.ExecuteReader();
                            while (srd.Read())
                            {
                                mainWindow.txtfullname.Text = srd.GetValue(0).ToString();

                                mainWindow.txtLevel.Text = "استاد";
                            }
                            srd.Close();
                            break;
                    }

                    mainWindow.dashboardZone.Visibility = Visibility.Visible;
                    mainWindow.profileZone.Visibility = Visibility.Visible;
                    mainWindow.DataContext = new HomeModel();
                    con.Close();
                   
                }
                else
                {
                    txtinfo.Text = "پسورد یا نام کاربری اشتباه میباشد";
                }

            }
          
            
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}