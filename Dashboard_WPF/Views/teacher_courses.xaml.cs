using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();
            SqlCommand userName =
                new SqlCommand(
                    $@"SELECT c.startDate, c.endDate, c.startTime, c.endTime, t.firstName, t.lastName, c.classLink
FROM Classes c
JOIN Teachers t ON c.teacherID = t.teacherID
WHERE c.courseID IN (SELECT co.courseID FROM Courses co WHERE co.courseName = '{coursename}');",
                    con);
            SqlDataReader srd = userName.ExecuteReader();

           // var courses = new List<string>();

            while (srd.Read())
            {
               


            }

            // var array = courses.ToArray();


        }

    }
}

