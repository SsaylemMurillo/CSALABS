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
    /// Interaction logic for ManagePatientPage.xaml
    /// </summary>
    public partial class ManagePatientPage : Page
    {
        public ManagePatientPage()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Edition Window: Pencil On Right Side Bar
            new EditPatientWindow().Show();
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            // Filter Window: Filter On Right Side Bar
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            // < Atras: Table 
        }

        private void Border_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            // 1: Table
        }

        private void Border_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            // 2: Table
        }

        private void Border_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            // 3: Table
        }

        private void Border_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            // 4: Table
        }
    }
}
