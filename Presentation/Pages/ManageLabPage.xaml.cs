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
    /// Interaction logic for ManageLabPage.xaml
    /// </summary>
    public partial class ManageLabPage : Page
    {

        public Frame MainFrame { get; set; }
        public Page PreviousPage { get; set; }

        private List<TextBox> registerTextBoxes;

        public ManageLabPage(Frame mainFrame, Page previousPage)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            PreviousPage = previousPage;
            registerTextBoxes = new List<TextBox>();
            FillTextBoxesList();
        }

        private void FillTextBoxesList()
        {
            registerTextBoxes.Add(idPatientTextBox);
            registerTextBoxes.Add(labPlaceTextBox);
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainPage(MainFrame));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(idPatientTextBox.Text) && idPatientTextBox.Text.Length > 0)
            {
                // Call to the lab - Patient services...


                // If the above action was ok... succesfull... unlock buttons
                chargeIdButton.IsEnabled = false;
                idPatientTextBox.IsEnabled = false;
                descriptionLABPAGETextBlock.Visibility = Visibility.Collapsed;
                saveLabButton.Visibility = Visibility.Visible;
                cancelLabButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Completa el campo N° de Identificacion del paciente para poder cargar",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
