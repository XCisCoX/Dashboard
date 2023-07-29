using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    ///
    public class WatchListModel : INotifyPropertyChanged
    {
        // Whatever properties here
        public string Id { get; set; }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }

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
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

            sd.SelectCommand = new SqlCommand(
                $@"select  cd.stuScore, s.firstName,s.lastName , s.studentID ,s.stuEmail,s.stuPhone from Classes c join ClassDetails cd on c.classID=cd.classID join Students s on  s.studentID=cd.studentID 
where (select co.courseID from Courses co where co.courseName='{coursename}')=c.courseID
order by c.classID", con);
            sd.Fill(ds);

            // var courses = new List<string>();
            dgMain.ItemsSource = ds.Tables[0].DefaultView;

         
        }
      
        

        private void dgMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // dgMain.SelectedCells
        }

        private void dgMain_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter();

         //   sd.SelectCommand = new SqlCommand(
             //   $@"", con);
               
             MessageBox.Show(dgMain.SelectedCells[0].ToString());
        }
    }
}

