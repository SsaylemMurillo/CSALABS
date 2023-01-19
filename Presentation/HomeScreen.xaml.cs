using Presentation.Pages;
using Presentation.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Lógica de interacción para HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        List<Control> modules;

        public HomeScreen()
        {
            modules = new List<Control>();
            InitializeComponent();
            MainFrame.Content = new MainPage(MainFrame);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Logout Session
            new MainWindow().Show();
            Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(txtSearch.Text) && txtSearch.Text.Length > 0)
            {
                textBlockSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBlockSearch.Visibility = Visibility.Visible;
            }
            */
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Configurations Window...", "CSALABS");
        }

        private void textBlockSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //txtSearch.Focus();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            //Main.Content = new MainPage();
            //pageTitle.Text = "CSA LABS";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MainPage(MainFrame);
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            sidePanelButton.Visibility = Visibility.Visible;
            borderSidePanel.Visibility = Visibility.Collapsed;
        }

        private void exitMenuButton_Click(object sender, RoutedEventArgs e)
        {
            sidePanelButton.Visibility = Visibility.Collapsed;
            borderSidePanel.Visibility = Visibility.Visible;
        }
    }
}
