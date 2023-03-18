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
using Presentation.Pages;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {
        public ManagePatientPage Page { get; set; }

        List<TextBox> editTextBoxes;
        public Patient MyPatient { get; set; }
        public int SelectedRow { get; set; }
        public PatientService MyService { get; set; }

        public EditPatientWindow(ManagePatientPage page, PatientService service, Patient patient, int rowSelected)
        {
            MyPatient = patient;
            SelectedRow = rowSelected;
            MyService = service;
            InitializeComponent();
            selectedRowTextBlock.Text = "" + (SelectedRow+1);
            editTextBoxes = new List<TextBox>();
            LoadFields();
            LoadFieldsValues();
            Page = page;
        }

        private void LoadFields()
        {
            editTextBoxes.Add(firstNameTextBox);
            editTextBoxes.Add(secondNameTextBox);
            editTextBoxes.Add(firstLastNameTextBox);
            editTextBoxes.Add(secondLastNameTextBox);
            editTextBoxes.Add(expeditionTextBox);
            editTextBoxes.Add(expeditionPlaceTextBox);
            editTextBoxes.Add(dateTextBox);
            editTextBoxes.Add(addressTextBox);
            editTextBoxes.Add(phoneTextBox);
            editTextBoxes.Add(idTypeTextBox);
            editTextBoxes.Add(nationalityTextBox);
        }

        private void LoadFieldsValues()
        {
            idTextBox.Text = "" + MyPatient.Id;
            idTextBox.IsEnabled = false;

            idTypeTextBox.Text = "" + MyPatient.IdType;
            idTypeTextBox.IsEnabled = false;

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
            
            dateTextBox.Text = "" + MyPatient.BornDate.Day + "/"
                + MyPatient.BornDate.Month + "/" + MyPatient.BornDate.Year;

            addressTextBox.Text = MyPatient.Address;
            addressTextBox.IsEnabled = false;

            phoneTextBox.Text = "" + MyPatient.Phone;
            phoneTextBox.IsEnabled = false;

            nationalityTextBox.Text = "" + MyPatient.Nacionality;
            nationalityTextBox.IsEnabled = false;

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
                MessageBoxButton.YesNo);

            if (value == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrEmpty(idTextBox.Text) && idTextBox.Text.Length > 0)
                {
                    var idValue = DataConversor.ConvertStringToInt(idTextBox.Text);

                    if (idValue != -1)
                    {
                        Patient patient = new Patient();

                        patient.Id = idValue;
                        var serviceResponse = MyService.DeletePatient(patient);
                        MessageBox.Show(serviceResponse.Message, "CSA LABS", MessageBoxButton.OK);
                        Page.LoadPatientDataGrid();
                        Close();
                    }
                    else
                    {
                        if (idValue == -1)
                        {
                            MessageBox.Show("El id que intentas ingresar no es válido", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                if (MessageBox.Show("Estás Seguro de querer editar a este paciente?", "CSA LABS",
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
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
                                dateBornString, dateExpeditionString, expeditionPlaceTextBox.Text, phoneValue,
                                addressTextBox.Text, nationalityTextBox.Text);
                            var serviceResponse = MyService.UpdatePatient(patient);
                            MessageBox.Show(serviceResponse.Message, "CSA LABS", MessageBoxButton.OK);
                            Page.LoadPatientDataGrid();
                            Close();
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
                    catch (Exception)
                    {
                        MessageBox.Show($"Ha ocurrido un error en la edición de la información no se editó el paciente", "CSA LABS", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Información incompleta, por favor, completa los campos", "CSA LABS", MessageBoxButton.OK);
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
