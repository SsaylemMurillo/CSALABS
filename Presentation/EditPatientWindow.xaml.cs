using Entity;
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
using BusinessLogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {

        public DataGrid TheDataGrid { get; set; }
        List<TextBox> editTextBoxes;
        public Patient MyPatient { get; set; }
        public int SelectedRow { get; set; }
        public PatientService MyService { get; set; }

        public EditPatientWindow(DataGrid dataGridPatient, PatientService service, Patient patient, int rowSelected)
        {
            MyPatient = patient;
            SelectedRow = rowSelected;
            MyService = service;
            InitializeComponent();
            selectedRowTextBlock.Text = "" + (SelectedRow+1);
            editTextBoxes = new List<TextBox>();
            LoadFields();
            LoadFieldsValues();
            TheDataGrid = dataGridPatient;
        }

        private void LoadFields()
        {
            editTextBoxes.Add(firstNameTextBox);
            editTextBoxes.Add(secondNameTextBox);
            editTextBoxes.Add(firstLastNameTextBox);
            editTextBoxes.Add(secondLastNameTextBox);
            editTextBoxes.Add(expeditionTextBox);
            editTextBoxes.Add(expeditionPlaceTextBox);
            editTextBoxes.Add(bornDateTextBox);
            editTextBoxes.Add(addressTextBox);
            editTextBoxes.Add(phoneTextBox);
        }

        private void LoadFieldsValues()
        {
            idTextBox.Text = "" + MyPatient.Id;
            idTextBox.IsEnabled = false;

            firstNameTextBox.Text = MyPatient.FirstName;
            firstNameTextBox.IsEnabled = false;

            secondNameTextBox.Text = MyPatient.SecondName;
            secondNameTextBox.IsEnabled = false;

            firstLastNameTextBox.Text = MyPatient.LastName;
            firstLastNameTextBox.IsEnabled = false;

            secondLastNameTextBox.Text = MyPatient.SecondLastName;
            secondLastNameTextBox.IsEnabled = false;

            expeditionTextBox.Text = "" + MyPatient.ExpeditionDate.Day + "/"
                + MyPatient.ExpeditionDate.Month + "/" + MyPatient.ExpeditionDate.Year;
            expeditionTextBox.IsEnabled = false;

            expeditionPlaceTextBox.Text = MyPatient.ExpeditionPlace;
            expeditionPlaceTextBox.IsEnabled = false;
            
            bornDateTextBox.Text = "" + MyPatient.BornDate.Day + "/"
                + MyPatient.BornDate.Month + "/" + MyPatient.BornDate.Year;

            addressTextBox.Text = MyPatient.Address;
            addressTextBox.IsEnabled = false;

            phoneTextBox.Text = "" + MyPatient.Phone;
            phoneTextBox.IsEnabled = false;

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
        private string stringDateFormat(string stringValue)
        {
            string newString = "";
            var stringArray = DateSeparator(stringValue);

            if (stringArray.Length > 0)
            {
                newString = stringArray[1] + "/" + stringArray[0] + "/" + stringArray[2];
            }
            return newString;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Edit Mode
            UnlockFields();
            editionPanel.Visibility = Visibility.Visible;
        }

        private void UnlockFields()
        {
            foreach (var item in editTextBoxes)
            {
                item.IsEnabled = true;
            }
        }

        private void LockFieldsAndRestaurationOfValues()
        {
            LoadFieldsValues();

            foreach (var item in editTextBoxes)
            {
                item.IsEnabled = false;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Delete Mode
            var value = MessageBox.Show("Estás Seguro de querer eliminar a este paciente?", "CSA LABS",
                MessageBoxButton.OKCancel);

            if (value == MessageBoxResult.OK)
            {
                // Create Patient Elimination Petition To the Database
                // Use the service...
                if (idTextBox.Text != null)
                {
                    Patient patient = new Patient();
                    patient.Id = int.Parse(idTextBox.Text);
                    var serviceResponse = MyService.DeletePatient(patient);
                    MessageBox.Show(serviceResponse.Message, "CSA LABS", MessageBoxButton.OK);
                    TheDataGrid.Items.Refresh();
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Create Patient Edition...
            // Send it to the database

            var value = MessageBox.Show("Estás Seguro de querer editar a este paciente?", "CSA LABS",
                MessageBoxButton.OKCancel);

            if (value == MessageBoxResult.OK)
            {
                // Create Patient Elimination Petition To the Database
                // Use the service...
                if (ValidateFields())
                {
                    Patient patient = new Patient(int.Parse(idTextBox.Text), "CC", firstNameTextBox.Text,
                    secondNameTextBox.Text, firstLastNameTextBox.Text, secondLastNameTextBox.Text,
                    stringDateFormat(bornDateTextBox.Text), stringDateFormat(expeditionTextBox.Text), expeditionPlaceTextBox.Text, int.Parse(phoneTextBox.Text),
                    addressTextBox.Text);
                    var serviceResponse = MyService.UpdatePatient(patient);
                    MessageBox.Show(serviceResponse.Message, "CSA LABS", MessageBoxButton.OK);
                    TheDataGrid.Items.Refresh();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Campos Vacíos Encontrados, Por Favor Completa La Información", "CSA LABS", MessageBoxButton.OK);
            }
        }

        private bool ValidateFields()
        {
            foreach (TextBox item in editTextBoxes)
            {
                if (string.IsNullOrEmpty(item.Text) && item.Text.Length <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            editionPanel.Visibility = Visibility.Collapsed;
            LockFieldsAndRestaurationOfValues();
        }
    }
}
