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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ConfigurationPage.xaml
    /// </summary>
    public partial class ConfigurationPage : Page
    {
        public ConfigurationPage()
        {
            InitializeComponent();
            MainFrame.Content = new MainSettingsPage();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new ThemesConfigPage();
        }

        private void MouseEnterEffect(Grid grid)
        {
            grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d9d9d9"));
            Cursor = Cursors.Hand;
        }

        private void MouseLeaveEffect(Grid grid)
        {
            grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            Cursor = Cursors.Arrow;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterEffect(themeButton);
        }

        private void themeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveEffect(themeButton);
        }

        private void analiticsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterEffect(analiticsButton);
        }

        private void analiticsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveEffect(analiticsButton);
        }

        private void accessButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterEffect(accessButton);
        }

        private void accessButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveEffect(accessButton);
        }

        private void securityButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseEnterEffect(securityButton);
        }

        private void securityButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeaveEffect(securityButton);
        }
    }
}
