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

namespace Dashboard_WPF.Views
{
  

    /// <summary>
    /// Interaction logic for contact.xaml
    /// </summary>
    public partial class contact : UserControl
    {
        public contact()
        {
            InitializeComponent();
        }
        Dictionary<string, string> iDictionary = new Dictionary<string, string>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();
            SqlCommand userName = new SqlCommand("SELECT [teacherID],CONCAT([firstName],' ',[lastName]) FROM [CollegeProject].[dbo].[Teachers]", con);
            SqlDataReader srd = userName.ExecuteReader();
      
            int counter = 0;
           
            while (srd.Read())
            {
                iDictionary[srd.GetValue(0).ToString()]= srd.GetValue(1).ToString();
                cbTeacher.Items.Add(iDictionary[srd.GetValue(0).ToString()]);
                counter++;
            }

         
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
        
            if (mainWindow != null){

                if (mainWindow.user_data["type"] == "2")
                {

                    string teacherName = cbTeacher.Text;
                    string teacherId = "";
                    foreach (KeyValuePair<string, string> VARIABLE in iDictionary)
                    {
                        if (VARIABLE.Value == teacherName)
                            teacherId = VARIABLE.Key;
                    }

                    SqlConnection con =
                        new SqlConnection(
                            "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
                    con.Open();

            

                    if (teacherId != "0")
                    {
                        string s =
                            $@"insert into stuMessages values({mainWindow.user_data["uid"]},{teacherId},N'{txtSubject.Text}',N'{txtText.Text}',GETDATE())";
                        SqlCommand sqlCommand = new SqlCommand(s, con);
                        try
                        {
                            SqlDataReader srd = sqlCommand.ExecuteReader();
                            srd.Close();
                            MessageBox.Show(
                                $"پیام شما با موفقیت ارسال شد");
                        }
                        catch (Exception exception)
                        {

                          
                        }

                    }
                }




            }
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
