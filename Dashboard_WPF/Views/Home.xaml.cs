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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }


        private void Card_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SqlConnection con =
                    new SqlConnection(
                        connectionclass.connectionstring);
                con.Open();
                string consrd = "";
                switch (mainWindow.user_data["type"])
                {
                    case "2":
                        consrd =
                            $"SELECT  sm.subject  ,sm.text ,sm.sentTime  ,CONCAT(st.firstname,' ',st.lastName) FROM stuMessages sm join Students t on t.studentID=sm.recieverID join Teachers st on st.teacherID=sm.senderID where  recieverID={mainWindow.user_data["uid"]}";

                        break;
                    case "3":
                        consrd =
                            $"SELECT  sm.subject  ,sm.text ,sm.sentTime  ,CONCAT(st.firstname,' ',st.lastName) FROM stuMessages sm join Teachers t on t.teacherID=sm.recieverID join Students st on st.studentID=sm.senderID where  recieverID={mainWindow.user_data["uid"]}";

                        break;
                    case "1":
                        break;
                }

                SqlCommand userName =
                    new SqlCommand(
                        consrd,
                        con);
                try
                {
                    SqlDataReader srd = userName.ExecuteReader();
                    int ig = 0;

                    List<Dictionary<string, string>> studList = new List<Dictionary<string, string>>();
                    while (srd.Read())
                    {
                        Dictionary<string, string> s = new Dictionary<string, string>();
                        s["subject"] = srd.GetValue(0).ToString();
                        s["text"] = srd.GetValue(1).ToString();
                        s["time"] = srd.GetValue(2).ToString();
                        s["sender"] = srd.GetValue(3).ToString();
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
 Mode=""Custom"" FlowDirection=""RightToLeft"" Background=""white"" Foreground=""black"" CornerRadius=""15""  Padding=""0 5 ""  materialDesign:ColorZoneAssist.Mode=""Dark"" >
                         <materialDesign:ColorZone  Background=""#e7efe1"" Foreground=""white"" CornerRadius=""15""  Padding=""10 10 10 10""  materialDesign:ColorZoneAssist.Mode=""Dark"">
                            <StackPanel  Background=""#e7efe1"">

                         <TextBlock HorizontalAlignment=""center"" FontSize=""16"" FontWeight=""SemiBold"" Foreground=""white""><Bold Foreground=""#168acd"">{VARIABLE["sender"]}</Bold></TextBlock>
                              
                                <DockPanel>

                                    <TextBlock HorizontalAlignment=""Left"" FontSize=""15"" FontWeight=""SemiBold"" ><Bold Foreground=""#191919"">{VARIABLE["subject"]}</Bold></TextBlock>

                                    <TextBlock HorizontalAlignment=""Right"" FontSize=""13"" FontWeight=""SemiBold"" ><Bold Foreground=""#a0aec9"">{VARIABLE["time"]}</Bold></TextBlock>
                              
                                </DockPanel>
                                  
                                <TextBlock  TextWrapping=""Wrap""  HorizontalAlignment=""Left"" FontSize=""14"" FontWeight=""SemiBold"" Foreground=""#000000"">{VARIABLE["text"]}</TextBlock>                           
</StackPanel>                               
                          
                          
                         </materialDesign:ColorZone>
                        </materialDesign:ColorZone>");
                        card = (MaterialDesignThemes.Wpf.ColorZone)XamlReader.Parse(sb.ToString());
                        //   card.MouseDown += Button_Click;
                        GridNotifications.Children.Add(card);
                    }
                }
                catch
                {

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SqlConnection con =
                    new SqlConnection(
                        connectionclass.connectionstring);
                con.Open();
                SqlCommand userName =
                    new SqlCommand(
                        $"delete stuMessages where recieverID={mainWindow.user_data["uid"]}",
                        con);
                SqlDataReader srd = userName.ExecuteReader();
                mainWindow.DataContext = new Home();
            }
        }
    }
}