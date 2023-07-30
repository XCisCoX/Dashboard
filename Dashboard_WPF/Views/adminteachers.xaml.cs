using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.Utils.Serializing.Helpers;
using MaterialDesignThemes.Wpf;


namespace Dashboard_WPF.Views
{
    /// <summary>
    /// Interaction logic for courses.xaml
    /// </summary>
    ///


    public partial class adminteachers : UserControl
    {
      

        public adminteachers()
        {
            
            InitializeComponent();
        }

      
        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"SELECT [teacherID] as 'آیدی'
      ,[firstName] as 'نام'
      ,[lastName] as 'نام خانوادگی'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      ,[nationalCod] as 'کد ملی'
      ,[tchAddress] as 'آدرس'
      ,[tchEmail] as 'ایمیل'
      ,[tchPhone] as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Teachers] join Accounts ac on ac.userID=teacherID  

", con);
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            dgMain.Columns[0].IsReadOnly = true;
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
                dgMain.Columns[i].IsReadOnly = false;
            }
            
         
        }
      
        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();

            bool success = true;
            for (int i = 0; i < dgMain.Items.Count-1; i++)
            {
                Dictionary<string, string> teachersDictionary = new Dictionary<string, string>();
                teachersDictionary["id"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[0].ToString();
                teachersDictionary["firstname"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[1].ToString();
                teachersDictionary["lastname"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[2].ToString();
                teachersDictionary["username"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[3].ToString();
                teachersDictionary["password"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[4].ToString();
                teachersDictionary["codemeli"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[5].ToString();
                teachersDictionary["address"]   = ((DataRowView)dgMain.Items[i]).Row.ItemArray[6].ToString();
                teachersDictionary["email"]     = ((DataRowView)dgMain.Items[i]).Row.ItemArray[7].ToString();
                teachersDictionary["phone"]     = ((DataRowView)dgMain.Items[i]).Row.ItemArray[8].ToString();

                if (teachersDictionary["id"]!="")
                {
                    string s = $@"update Teachers
set firstName='{teachersDictionary["firstname"]}',lastName='{teachersDictionary["lastname"]}',
nationalCod='{teachersDictionary["codemeli"]}',tchAddress='{teachersDictionary["address"]}',
tchEmail='{teachersDictionary["email"]}',tchPhone='{teachersDictionary["phone"]}'
where teacherID={teachersDictionary["id"]}
update Accounts
set userName='{teachersDictionary["username"]}' ,userPass='{teachersDictionary["password"]}'
where userID={teachersDictionary["id"]}
";
                SqlCommand sqlCommand = new SqlCommand(s, con);
                try
                {
                    SqlDataReader srd = sqlCommand.ExecuteReader();
                    srd.Close();
               
                }
                catch (Exception exception)
                {
                    
                    success = false;
                }
            
                }
                else
                {
                    if (teachersDictionary["firstname"] != "" &&
                        teachersDictionary["lastname"] != "" &&
                        teachersDictionary["username"] != "" &&
                        teachersDictionary["password"] != "" &&
                        teachersDictionary["codemeli"] != "" &&
                        teachersDictionary["address"] != "" &&
                        teachersDictionary["email"] != "" &&
                        teachersDictionary["phone"] != "")
                    {
                        string s = $@"DECLARE @tid int;
SET @tid=(select top (1) teacherID+1 from Teachers order by teacherID desc)
insert into Teachers 
values (@tid,'{teachersDictionary["firstname"]}',
'{teachersDictionary["lastname"]}','{teachersDictionary["codemeli"]}','{teachersDictionary["address"]}',
'{teachersDictionary["email"]}',{teachersDictionary["phone"]})
insert into Accounts 
values (@tid,'{teachersDictionary["username"]}','{teachersDictionary["password"]}',3,'')
";

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
                    }
                    else
                    {
                        success = false;
                    }
                }


            }
    
 
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"SELECT [teacherID] as 'آیدی'
      ,[firstName] as 'نام'
      ,[lastName] as 'نام خانوادگی'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      ,[nationalCod] as 'کد ملی'
      ,[tchAddress] as 'آدرس'
      ,[tchEmail] as 'ایمیل'
      ,[tchPhone] as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Teachers] join Accounts ac on ac.userID=teacherID  

", con);
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            dgMain.Columns[0].IsReadOnly = true;
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
                dgMain.Columns[i].IsReadOnly = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();

            string s = $@"delete from Teachers where teacherID={txtDelTeacher.Text}
            ";
            bool success =true;
            SqlCommand sqlCommand = new SqlCommand(s, con);
            try
            {
                SqlDataReader srd = sqlCommand.ExecuteReader();
                srd.Close();
                
            }
            catch
            {
                success=false;
            }
      
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"SELECT [teacherID] as 'آیدی'
      ,[firstName] as 'نام'
      ,[lastName] as 'نام خانوادگی'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      ,[nationalCod] as 'کد ملی'
      ,[tchAddress] as 'آدرس'
      ,[tchEmail] as 'ایمیل'
      ,[tchPhone] as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Teachers] join Accounts ac on ac.userID=teacherID  

", con);
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            dgMain.Columns[0].IsReadOnly = true;
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
                dgMain.Columns[i].IsReadOnly = false;
            }
            txtDelTeacher.Text = "";
        }
    }
}

