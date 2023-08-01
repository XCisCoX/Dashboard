using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
        private string typecase = "";

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();

            if (mainWindow != null)
            {
                typecase = mainWindow.user_data["type"];




                string messager ="";
                switch (typecase)
                {

                    case "2":
                        txtsendto.Text = "ارسال پیام به استاد";
                        messager =
                            $"select t.teacherID,concat(t.firstName,' ',t.lastName) from teachers t join Classes c on t.teacherID=c.teacherID join ClassDetails cd on c.classID=cd.classID join Students s on s.studentID=cd.studentID\r\nwhere s.studentID={mainWindow.user_data["uid"]}";
                        break;

                    case "3":
                        txtsendto.Text = "ارسال پیام به دانشجو";
                        messager =
                            $"select s.studentID ,concat(s.firstName,' ',s.lastName)  from Students s join ClassDetails cd on s.studentID=cd.studentID join Classes c on cd.classID=c.classID where c.teacherID={mainWindow.user_data["uid"]}";
                        break;
                }

                SqlCommand userName =
                    new SqlCommand(
                        messager,
                        con);
                SqlDataReader srd = userName.ExecuteReader();

                int counter = 0;

                while (srd.Read())
                {
                    iDictionary[srd.GetValue(0).ToString()] = srd.GetValue(1).ToString();
                    cbTeacher.Items.Add(iDictionary[srd.GetValue(0).ToString()]);
                    counter++;
                }

            }
        }






        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
        
            if (mainWindow != null){


                try
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
                            connectionclass.connectionstring);
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
                catch
                {

                }




            }
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
