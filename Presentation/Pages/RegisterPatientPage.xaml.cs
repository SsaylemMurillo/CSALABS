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
            registerTextBoxes.Add(nationalityTextBox);
            registerTextBoxes.Add(idTypeTextBox);
        }

        private void CleanFields()
        {
            foreach (TextBox item in registerTextBoxes)
            {
                item.Text = null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CleanFields();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                try
                {
                    var idValue = DataConversor.ConvertStringToInt(idTextBox.Text);
                    var phoneValue = DataConversor.ConvertStringToInt(phoneTextBox.Text);
                    var dateBornString = DataConversor.ConvertStringToDateFormat(dateTextBox.Text);
                    var dateExpeditionString = DataConversor.ConvertStringToDateFormat(expeditionTextBox.Text);

                    if (idValue != -1 && phoneValue != -1 && dateBornString != "" && dateExpeditionString != "")
                    {
                        Patient patient = new Patient(idValue, idTypeTextBox.Text, firstNameTextBox.Text,
                        secondNameTextBox.Text, firstLastNameTextBox.Text, secondLastNameTextBox.Text,
                        dateBornString, dateExpeditionString, placeExpeditionTextBox.Text, phoneValue,
                        addressTextBox.Text, nationalityTextBox.Text);

                        string message = MyPatientService.SavePatient(patient).Message;
                        MessageBox.Show(message, "CSA LABS");
                        CleanFields();
                    }
                    else
                    {
                        if (idValue == -1)
                        {
                            MessageBox.Show("El id que intentas ingresar no es válido", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (phoneValue == -1)
                        {
                            MessageBox.Show("El telefono que intentas ingresar no es válido", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (dateBornString == "")
                        {
                            MessageBox.Show("La fecha de nacimiento que intentas ingresar no es válida", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (dateExpeditionString == "")
                        {
                            MessageBox.Show("La fecha de expedición que intentas ingresar no es válida", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
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
            MainFrame.NavigationService.Navigate(PreviousPage);
        }
    }
}
