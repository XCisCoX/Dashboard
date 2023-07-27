using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class student_shahrie : UserControl
    {
        public student_shahrie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        string checkstatus(string status)
        {
            if (status == "1")
                return "پرداخت شده";
            else
                return "نیاز به پرداخت ";
        }

    private void Card_Loaded(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SqlConnection con =
                    new SqlConnection(
                        "Data Source=DESKTOP-G8PIQ1K;Initial Catalog=CollegeProject;Integrated Security=True");
                con.Open();
                SqlCommand userName =
                    new SqlCommand(
                        $"SELECT  [studentID] ,[tuitionStatus] ,[paidAmnt] ,[remainingAmnt],[term]  FROM [CollegeProject].[dbo].[Tuition] where studentID={mainWindow.user_data["uid"]}",
                        con);
                SqlDataReader srd = userName.ExecuteReader();
                int ig = 0;

                List<Dictionary<string, string>> studList = new List<Dictionary<string, string>>();
                while (srd.Read())
                {
                    Dictionary<string, string> s = new Dictionary<string, string>();
                    s["status"] = srd.GetValue(1).ToString();
                    s["paidamount"] = srd.GetValue(2).ToString();
                    s["remainingamount"] = srd.GetValue(3).ToString();
                    s["term"] = srd.GetValue(4).ToString();
                    studList.Add(s);

                 
                }
                foreach (var VARIABLE in studList)
                {
                    MaterialDesignThemes.Wpf.ColorZone card = new MaterialDesignThemes.Wpf.ColorZone();
                    StringBuilder sb = new StringBuilder();
                    ig += 1;
                    //Create card
                    sb.Append($@"   
                            <materialDesign:ColorZone
xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"" 
             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
             xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:local=""clr-namespace:Dashboard"" Grid.Column=""1""
Grid.Row=""{ig}"" Mode=""Custom"" FlowDirection=""RightToLeft"" Background=""White"" Foreground=""Black"" CornerRadius=""15"" Padding=""20 20"">
                            <StackPanel>
       
                                <DockPanel>
                                    <TextBlock HorizontalAlignment=""Left"" FontSize=""11"" FontWeight=""SemiBold"" Foreground=""Gray""><Bold Foreground=""Black"">{checkstatus(VARIABLE["status"])}</Bold></TextBlock>

                              
                                </DockPanel>
                                <TextBlock HorizontalAlignment=""Left"" FontSize=""11"" FontWeight=""SemiBold"" Foreground=""DimGray"">شما در ترم {VARIABLE["term"]}مبلغ {VARIABLE["paidamount"]}تومان پرداخت کردید و {VARIABLE["remainingamount"]} مانده است و نیاز به پرداخت دارید</TextBlock>

                            </StackPanel>
                        </materialDesign:ColorZone>");
                    card = (MaterialDesignThemes.Wpf.ColorZone)XamlReader.Parse(sb.ToString());
                 //   card.MouseDown += Button_Click;
                    container.Children.Add(card);
                    if (VARIABLE["status"] == "1")
                        btnPardakht.IsEnabled = false;
                    else
                    {
                        this.boardzone.Background = new SolidColorBrush(Color.FromRgb(160, 29, 19));
                        txtShahrie.Text = "شما شهریه پرداخت نشده ای دارید!";
                    }
                }
            }


        }
    }
}
