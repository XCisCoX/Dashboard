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


    public partial class adminstudents : UserControl
    {
      

        public adminstudents()
        {
            
            InitializeComponent();
        }

      
        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"SELECT st.studentID as 'آیدی'
      , st.firstName as 'نام'
      , st.lastName as 'نام خانوادگی'
	  ,case when tu.tuitionStatus = 0 then N'پرداخت نشده'
            when tu.tuitionStatus = 1 then N'نیمه پرداختی'
            when tu.tuitionStatus = 2 then N'پرداخت شده' 
        end as 'وضعیت شهریه'
      ,tu.paidAmnt as 'پرداخت کرده'
	  ,tu.remainingAmnt as 'نیاز به پرداخت'
	  ,tu.term as 'ترم'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      , st.nationalCod as 'کد ملی'
      , st.stuAddress as 'آدرس'
      , st.stuEmail as 'ایمیل'
      , st.stuPhone as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Students] st  join Accounts ac on ac.userID=studentID join Tuition tu on tu.studentID= st.studentID
", con)
            {

            };
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
               
                dgMain.Columns[i].IsReadOnly = false;
            }
            dgMain.Columns[0].IsReadOnly = true;
            dgMain.Columns[4].IsReadOnly = true;
            dgMain.Columns[3].IsReadOnly = true;
            dgMain.Columns[5].IsReadOnly = true;
            dgMain.Columns[6].IsReadOnly = true;
        }
      
        

   

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();

            bool success = true;
            for (int i = 0; i < dgMain.Items.Count-1; i++)
            {
                Dictionary<string, string> teachersDictionary = new Dictionary<string, string>();
                teachersDictionary["id"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[0].ToString();
                teachersDictionary["firstname"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[1].ToString();
                teachersDictionary["lastname"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[2].ToString();
                teachersDictionary["status"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[3].ToString();
                teachersDictionary["paidamnt"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[4].ToString();
                teachersDictionary["remainingamnt"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[5].ToString();
                teachersDictionary["term"] = ((DataRowView)dgMain.Items[i]).Row.ItemArray[6].ToString();
                teachersDictionary["username"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[7].ToString();
                teachersDictionary["password"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[8].ToString();
                teachersDictionary["codemeli"]  = ((DataRowView)dgMain.Items[i]).Row.ItemArray[9].ToString();
                teachersDictionary["address"]   = ((DataRowView)dgMain.Items[i]).Row.ItemArray[10].ToString();
                teachersDictionary["email"]     = ((DataRowView)dgMain.Items[i]).Row.ItemArray[11].ToString();
                teachersDictionary["phone"]     = ((DataRowView)dgMain.Items[i]).Row.ItemArray[12].ToString();

                if (teachersDictionary["id"]!="")
                {
                    string s = $@"update Students
set firstName='{teachersDictionary["firstname"]}',lastName='{teachersDictionary["lastname"]}',
nationalCod='{teachersDictionary["codemeli"]}',stuAddress='{teachersDictionary["address"]}',
stuEmail='{teachersDictionary["email"]}',stuPhone='{teachersDictionary["phone"]}'
where studentID={teachersDictionary["id"]}
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
                        teachersDictionary["phone"] != ""
                       )
                    {
                        string s = $@"DECLARE @tid int;
SET @tid=(select top (1) studentID+1 from Students order by studentID desc)
insert into Students 
values (@tid,'{teachersDictionary["firstname"]}',
'{teachersDictionary["lastname"]}','{teachersDictionary["codemeli"]}','{teachersDictionary["address"]}',
'{teachersDictionary["email"]}',{teachersDictionary["phone"]})
insert into Accounts 
values (@tid,'{teachersDictionary["username"]}','{teachersDictionary["password"]}',2,'')
insert into Tuition
values (@tid,'0','0','100000','1')
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
                $@"SELECT st.studentID as 'آیدی'
      , st.firstName as 'نام'
      , st.lastName as 'نام خانوادگی'
	  ,case when tu.tuitionStatus = 0 then N'پرداخت نشده'
            when tu.tuitionStatus = 1 then N'نیمه پرداختی'
            when tu.tuitionStatus = 2 then N'پرداخت شده' 
        end as 'وضعیت شهریه'
      ,tu.paidAmnt as 'پرداخت کرده'
	  ,tu.remainingAmnt as 'نیاز به پرداخت'
	  ,tu.term as 'ترم'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      , st.nationalCod as 'کد ملی'
      , st.stuAddress as 'آدرس'
      , st.stuEmail as 'ایمیل'
      , st.stuPhone as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Students] st  join Accounts ac on ac.userID=studentID join Tuition tu on tu.studentID= st.studentID
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
                    connectionclass.connectionstring);
            con.Open();

            string s = $@"delete Students where studentID={txtDelStudent.Text}";
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
                $@"SELECT st.studentID as 'آیدی'
      , st.firstName as 'نام'
      , st.lastName as 'نام خانوادگی'
	  ,case when tu.tuitionStatus = 0 then N'پرداخت نشده'
            when tu.tuitionStatus = 1 then N'نیمه پرداختی'
            when tu.tuitionStatus = 2 then N'پرداخت شده' 
        end as 'وضعیت شهریه'
      ,tu.paidAmnt as 'پرداخت کرده'
	  ,tu.remainingAmnt as 'نیاز به پرداخت'
	  ,tu.term as 'ترم'
	  ,ac.userName as 'نام کاربری'
	  ,ac.userPass as 'پسورد'
      , st.nationalCod as 'کد ملی'
      , st.stuAddress as 'آدرس'
      , st.stuEmail as 'ایمیل'
      , st.stuPhone as 'شماره همراه'
  FROM [CollegeProject].[dbo].[Students] st  join Accounts ac on ac.userID=studentID join Tuition tu on tu.studentID= st.studentID
", con);
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            dgMain.Columns[0].IsReadOnly = true;
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
                dgMain.Columns[i].IsReadOnly = false;
            }
            txtDelStudent.Text = "";
        }
    }
}

