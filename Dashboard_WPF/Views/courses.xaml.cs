using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class courses : UserControl
    {
        public courses()
        {
            InitializeComponent();
        }

        private void Card_Loaded(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    MaterialDesignThemes.Wpf.ColorZone card = new MaterialDesignThemes.Wpf.ColorZone();
                    StringBuilder sb = new StringBuilder();

                    //Create card
                    sb.Append(
                        $@"<materialDesign:ColorZone  xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"" 
             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
             xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:local=""clr-namespace:Dashboard""
             Grid.Column=""{j.ToString()}""
             Grid.Row=""{i.ToString()}""
             CornerRadius=""15"" Height=""65"" Mode=""Custom""
             Background=""CornflowerBlue"" Margin=""0 0 15 15"" Foreground=""White"" Padding=""20 10"">
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width=""23*""/>
                                <ColumnDefinition Width=""52*""/>
                            </Grid.ColumnDefinitions>
                            <materialDesign:PackIcon Grid.Column=""0"" Kind=""Pencil"" Height=""30"" Width=""30"" HorizontalAlignment=""Left"" VerticalAlignment=""Center"" Foreground=""White"" Margin=""0 8 0 7""/>
                            <StackPanel Grid.Column=""1"" VerticalAlignment=""Center"" Height=""31"" Margin=""0 7"">
                                <TextBlock FlowDirection=""RightToLeft"" Text=""Common English"" FontSize=""11"" FontWeight=""Regular""/>
                                <TextBlock FlowDirection=""RightToLeft"" Text=""Phrasal Verbs"" FontSize=""12"" FontWeight=""Bold""/>
                            </StackPanel>
</Grid></materialDesign:ColorZone>");
                    card = (MaterialDesignThemes.Wpf.ColorZone)XamlReader.Parse(sb.ToString());
                    container.Children.Add(card);
                }

            }
        }
    }
}
