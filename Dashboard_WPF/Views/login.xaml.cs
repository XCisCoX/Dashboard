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
                string userInfo="";
                bool isLogin = false;
                while (srd.Read())
                {
                    mainWindow.user_data["uid"] = srd.GetValue(0).ToString();
                    mainWindow.user_data["username"]=srd.GetValue(1).ToString();
                    mainWindow.user_data["password"] = srd.GetValue(2).ToString();
                    mainWindow.user_data["type"] = srd.GetValue(3).ToString();
                    mainWindow.user_data["avatar"]=srd.GetValue(4).ToString();
                    isLogin=true;
                }
                srd.Close();
                SqlCommand uinfCommand;

                if (isLogin)
                {
                    txtinfo.Text = "";
                    switch (mainWindow.user_data["type"])
                    {
                        case "2":
                            userInfo = $@"SELECT 
      [studentID]
      ,[firstName]
      ,[lastName]
      ,[nationalCod]
      ,[stuAddress]
      ,[stuEmail]
      ,[stuPhone]
  FROM [CollegeProject].[dbo].[Students] where studentID={mainWindow.user_data["uid"]}
";

                            uinfCommand = new SqlCommand(userInfo, con);
                            srd = uinfCommand.ExecuteReader();
                            while (srd.Read())
                            {
                                mainWindow.user_data["firstname"] = srd.GetValue(1).ToString();
                                mainWindow.user_data["lastname"]=srd.GetValue(2).ToString();
                                mainWindow.user_data["codemeli"] = srd.GetValue(3).ToString();
                                mainWindow.user_data["address"] = srd.GetValue(4).ToString();
                                mainWindow.user_data["email"]=srd.GetValue(5).ToString();
                                mainWindow.user_data["phone"]=srd.GetValue(6).ToString();

                            }
                            srd.Close();
                            mainWindow.txtfullname.Text = mainWindow.user_data["firstname"] + " " +
                                                          mainWindow.user_data["lastname"];
                                
                            mainWindow.txtLevel.Text = "دانشجو";
                            break;
                        case "3":
                            userInfo = $@"SELECT 
   [teacherID]
      ,[firstName]
      ,[lastName]
      ,[nationalCod]
      ,[tchAddress]
      ,[tchEmail]
      ,[tchPhone]
  FROM [CollegeProject].[dbo].[Teachers] where teacherID={mainWindow.user_data["uid"]}
";


                            uinfCommand = new SqlCommand(userInfo, con);
                            srd = uinfCommand.ExecuteReader();
                            while (srd.Read())
                            {
                                mainWindow.user_data["firstname"] = srd.GetValue(1).ToString();
                                mainWindow.user_data["lastname"] = srd.GetValue(2).ToString();
                                mainWindow.user_data["codemeli"] = srd.GetValue(3).ToString();
                                mainWindow.user_data["address"] = srd.GetValue(4).ToString();
                                mainWindow.user_data["email"] = srd.GetValue(5).ToString();
                                mainWindow.user_data["phone"] = srd.GetValue(6).ToString();

                            }
                            srd.Close();
                            mainWindow.txtfullname.Text = mainWindow.user_data["firstname"] + " " +
                                                          mainWindow.user_data["lastname"];
                            mainWindow.shahrieitem.Visibility = Visibility.Collapsed;
                            mainWindow.txtLevel.Text = "استاد";
                            mainWindow.txtContactwith.Text = "تماس دانشجو";
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