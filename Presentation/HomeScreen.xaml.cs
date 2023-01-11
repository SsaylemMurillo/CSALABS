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
            Main.Content = new MainPage();
            GetModules();
            LoadModules();
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

        public void GetModules()
        {
            // Communicate with the user-rol database and get the modules

            ButtonMenuController menuController = new ButtonMenuController();
            menuController.ChangeTextOfButton("Registro Paciente");
            menuController.ChangeImageOfButton("Images/user.png");
            menuController.MainFrame = Main;
            menuController.TextFrame = pageTitle;

            ButtonMenuController menuController2 = new ButtonMenuController();
            menuController2.ChangeTextOfButton("Gestión Paciente");
            menuController2.ChangeImageOfButton("Images/user_m.png");
            menuController2.PageOfButton = new MainPage();
            menuController2.MainFrame = Main;
            menuController2.TextFrame = pageTitle;

            modules.Add(menuController);
            modules.Add(menuController2);

        }

        public void LoadModules()
        {
            foreach (ButtonMenuController item in modules)
            {
                myListBoxModules.Items.Add(item);
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Logout Session
            new MainWindow().Show();
            Close();
        }
    }
}
