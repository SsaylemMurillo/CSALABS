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
    /// Interaction logic for RegisterPatientPage.xaml
    /// </summary>
    public partial class RegisterPatientPage : Page
    {
        List<TextBox> registerTextBoxes;

        public RegisterPatientPage()
        {
            InitializeComponent();
            ChargeRegisterInformation();
            registerTextBoxes = new List<TextBox>();
            FillTextBoxesList();
        }

        
        public void ChargeRegisterInformation()
        {
            registerTextInformation.Text = "Lorem Ipsum is simply dummy text of the printing and " +
                "typesetting industry. Lorem Ipsum has been the industry's standard dummy text " +
                "ever since the 1500s, when an unknown printer took a galley of type and scrambled" +
                " it to make a type specimen book.\r\n";
        }

        private void FillTextBoxesList()
        {
            registerTextBoxes.Add(firstNameTextBox);
            registerTextBoxes.Add(secondNameTextBox);
            registerTextBoxes.Add(firstLastNameTextBox);
            registerTextBoxes.Add(secondLastNameTextBox);
            registerTextBoxes.Add(idTextBox);
            registerTextBoxes.Add(expeditionTextBox);
            registerTextBoxes.Add(placeExpeditionTextBox);
            registerTextBoxes.Add(dateTextBox);
            registerTextBoxes.Add(addressTextBox);
            registerTextBoxes.Add(phoneTextBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Clean Fields

            foreach (TextBox item in registerTextBoxes)
            {
                item.Text = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Create Patient
            if (ValidateFields())
            {
                // Patient Object Creation...
            }
            else
            {
                MessageBox.Show("Campos Vacios Encontrados ! Revisa la información digitada.", "CSA LABS");
            }
            
        }

        private bool ValidateFields()
        {
            foreach (TextBox item in registerTextBoxes)
            {
                if (string.IsNullOrEmpty(item.Text) && item.Text.Length <= 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
