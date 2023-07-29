using System;
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
    public class stuClass
    {
        public int score { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
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


        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }




        private void dgMain_TextInput(object sender, TextCompositionEventArgs e)
        {
         
        }

        private void dgMain_TargetUpdated(object sender, DataTransferEventArgs e)
        {
        

        }

        private void dgMain_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            SqlConnection con =
                new SqlConnection(
                    "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
            con.Open();
            
         

            //   sd.SelectCommand = new SqlCommand(
            //   $@"", con);

            for (int i = 0; i < dgMain.SelectedCells.Count; i++)
            {
                string score = ((DataRowView)dgMain.SelectedCells[i].Item).Row.ItemArray[0].ToString();
                string studentId = ((DataRowView)dgMain.SelectedCells[i].Item).Row.ItemArray[3].ToString();
                string s = $@"";
                SqlCommand sqlCommand = new SqlCommand(s, con);
                SqlDataReader srd = sqlCommand.ExecuteReader();




            }


        }
    }
}

