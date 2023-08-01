using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
using MaterialDesignThemes.Wpf;


namespace Dashboard_WPF.Views
{
    /// <summary>
    /// Interaction logic for courses.xaml
    /// </summary>
    ///
     public class ClassDetails
    {
    public string CourseName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string TeacherName { get; set; }
    public string ClassLink { get; set; }
    public string Score { get; set; }
    }
public partial class student_courses : UserControl
    {
        private string coursename = "";

        public student_courses(string cName)
        {
            this.coursename = cName;
            InitializeComponent();
        }
       
        private void Card_Loaded(object sender, RoutedEventArgs e)
        {

            SqlConnection con =
                new SqlConnection(
                    connectionclass.connectionstring);
            con.Open();
            SqlCommand userName =
                new SqlCommand(
                    $@"SELECT c.startDate, c.endDate, c.startTime, c.endTime, t.firstName, t.lastName, c.classLink, cd.stuScore
FROM Classes c 
JOIN Teachers t ON c.teacherID = t.teacherID JOIN ClassDetails cd on cd.classID =c.classID
WHERE c.courseID IN (SELECT co.courseID FROM Courses co WHERE co.courseName = '{coursename}')",
                    con);
            SqlDataReader srd = userName.ExecuteReader();

           // var courses = new List<string>();

            while (srd.Read())
            {
                var classDetails = new ClassDetails
                {
                    CourseName = coursename,
                    StartDate = srd.GetValue(0).ToString(),
                    EndDate = srd.GetValue(1).ToString(),
                    StartTime = srd.GetValue(2).ToString(),
                    EndTime = srd.GetValue(3).ToString(),
                    TeacherName = srd.GetValue(4).ToString() +" "+ srd.GetValue(5).ToString(),
                    ClassLink = srd.GetValue(6).ToString(),
                    Score = srd.GetValue(7).ToString()
                };
                DataContext = classDetails;
     


            }

            // var array = courses.ToArray();


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = (sender as Button).Tag as string;
            OpenLink(url);
        }

        private void OpenLink(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

