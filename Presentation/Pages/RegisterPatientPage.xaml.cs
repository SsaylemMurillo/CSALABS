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
using BusinessLogicLayer;
using Entity;

namespace Presentation.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPatientPage.xaml
    /// </summary>
    public partial class RegisterPatientPage : Page
    {
        public Page PreviousPage { get; set; }
        public Frame MainFrame { get; set; }
        public PatientService MyPatientService { get; set; }
        List<TextBox> registerTextBoxes;

        public RegisterPatientPage(Frame mainFrame, Page previousPage)
        {
            InitializeComponent();
            ChargeRegisterInformation();
            registerTextBoxes = new List<TextBox>();
            MyPatientService = new PatientService(ConnectionStringExtractor.connectionString);
            FillTextBoxesList();
            MainFrame = mainFrame;
            PreviousPage = previousPage;
        }


        public void ChargeRegisterInformation()
        {
            registerTextInformation.Text = "Lorem Ipsum is simply dummy text of the printing and " +
                "typesetting industry. Lorem Ipsum has been the industry's standard dummy text " +
                "ever since the 1500s, when an unknown printer took a galley of type and scrambled" +
                " it to make a type specimen book.\r\n";
        }

        public string[] DateSeparator(string stringValue)
        {
            if (stringValue != null)
            {
                var strings = stringValue.Split('/');
                return strings;
            }
            else
            {
                return null;
            }

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

        private string stringDateFormat(string stringValue)
        {
            string newString = "";
            var stringArray = DateSeparator(stringValue);

            if (stringArray.Length > 0)
            {
                newString = stringArray[1] + "/" + stringArray[0] + "/" + stringArray[2];
            }return newString;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Create Patient
            if (ValidateFields())
            {
                // Patient Object Creation...
                try
                {
                    Patient patient = new Patient(int.Parse(idTextBox.Text), "CC", firstNameTextBox.Text,
                    secondNameTextBox.Text, firstLastNameTextBox.Text, secondLastNameTextBox.Text,
                    stringDateFormat(dateTextBox.Text), stringDateFormat(expeditionTextBox.Text), placeExpeditionTextBox.Text, int.Parse(phoneTextBox.Text),
                    addressTextBox.Text);

                    string message = MyPatientService.SavePatient(patient).Message;
                    MessageBox.Show(message, "CSA LABS");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "CSA LABS");
                }
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = PreviousPage;
        }
    }
}
