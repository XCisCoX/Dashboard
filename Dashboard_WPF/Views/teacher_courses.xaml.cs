﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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


    public partial class teacher_courses : UserControl
    {
        private string coursename = "";

        public teacher_courses(string cName)
        {
            this.coursename = cName;
            InitializeComponent();
        }

      
        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
            txtCourseName.Text= coursename;
            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"select  cd.stuScore as 'نمره', s.firstName as 'نام',s.lastName as 'نام خانوادگی', s.studentID as 'شماره دانشجویی' ,s.stuEmail as 'ایمیل',s.stuPhone as 'شماره همراه' from Classes c join ClassDetails cd on c.classID=cd.classID join Students s on  s.studentID=cd.studentID 
where (select co.courseID from Courses co where co.courseName='{coursename}')=c.courseID
order by c.classID", con);
            sd.Fill(ds);


            dgMain.ItemsSource = ds.Tables[0].DefaultView;
            dgMain.Columns[0].IsReadOnly = false;
            for (int i = 1; i < dgMain.Columns.Count; i++)
            {
                dgMain.Columns[i].IsReadOnly = true;
            }
            
         
        }
      
        

        private void dgMain_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
           


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();

            bool success = true;
            for (int i = 0; i < dgMain.Items.Count; i++)
            {

                string score = ((DataRowView)dgMain.Items[i]).Row.ItemArray[0].ToString();
                string studentId = ((DataRowView)dgMain.Items[i]).Row.ItemArray[3].ToString();
                if(score!="")
                {
                    string s = $@"update classdetails
set stuScore = {score}
where studentID= {studentId}
and  classID=(select co.classID from Classes co where co.courseID=(select c.courseID from Courses c where c.courseName='{coursename}'))";
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


            }
            if(success)
            MessageBox.Show("اطلاعات با موفقیت در ذخیره شد", "پیام",
                MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                MessageBox.Show("مشکلی در ذخیره سازی اطلاعات وجود دارد!", "پیام",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("لینک کلاس در حال باز شدن میباشد", "پیام",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

