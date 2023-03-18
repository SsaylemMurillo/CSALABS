using BusinessLogicLayer;
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

        public PatientService MyPatientService { get; set; }

        public LaboratoryService MyLabService { get; set; }

        public ManageLabPage(Frame mainFrame, Page previousPage)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            PreviousPage = previousPage;
            registerTextBoxes = new List<TextBox>();
            FillTextBoxesList();
            MyPatientService = new PatientService(ConnectionStringExtractor.connectionString);
            MyLabService = new LaboratoryService(ConnectionStringExtractor.connectionString);
        }

        private void FillTextBoxesList()
        {
            registerTextBoxes.Add(idPatientTextBox);
            //registerTextBoxes.Add(labPlaceTextBox);
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainPage(MainFrame));
        }

        public void LoadLaboratoryDataGrid(List<Laboratory> labs)
        {
            labsDataGrid.Items.Clear();

            if (labs != null)
            {
                foreach (Laboratory item in labs)
                {
                    labsDataGrid.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            labsDataGrid.Items.Clear();
            if (!string.IsNullOrEmpty(idPatientTextBox.Text) && idPatientTextBox.Text.Length > 0)
            {
                // Call to the lab - Patient services...
                Patient patient = new Patient();
                var idValue = DataConversor.ConvertStringToInt(idPatientTextBox.Text);
                
                if(idValue != -1)
                {
                    patient.Id = idValue;
                    var response = MyPatientService.SearchPatient(patient);
                    if (response.ObjectResponse != null)
                    {
                        var response2 = MyLabService.SearchLaboratories(patient);
                        if (response2.DataList != null)
                        {
                            LoadLaboratoryDataGrid(response2.DataList);
                            ModifyInterface();
                        }
                        else
                        {
                            var message = MessageBox.Show("El paciente no tiene laboratorios, ¿ Desea crear nuevo laboratorio?",
                            "CSA LABS",
                            MessageBoxButton.YesNo, MessageBoxImage.Information);
                            if (message == MessageBoxResult.Yes)
                            {
                                new CreateLabWindow(this, MyLabService, new Laboratory()
                                {
                                    Patient = patient,
                                },
                                labsDataGrid.SelectedIndex).ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        var message = MessageBox.Show("El id paciente NO existe, ¿ Desea crear un nuevo paciente ?",
                        "CSA LABS",
                        MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (message == MessageBoxResult.Yes)
                        {
                            MainFrame.NavigationService.Navigate(new RegisterPatientPage(MainFrame, this));
                        }
                    }
                }
                else
                {
                    if (idValue == -1)
                    {
                        MessageBox.Show("El id que intentas ingresar no es válido", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Completa el campo N° de Identificacion del paciente para poder cargar",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ModifyInterface()
        {
            // If the above action was ok... succesfull... unlock buttons
            // descriptionLABPAGETextBlock.Visibility = Visibility.Collapsed;
            // saveLabButton.Visibility = Visibility.Visible;
            // cancelLabButton.Visibility = Visibility.Visible;
        }

        private void pencilButton_Click(object sender, RoutedEventArgs e)
        {
            if (labsDataGrid.Items.Count > 0)
            {
                var laboratory = (Laboratory)labsDataGrid.SelectedItem;
                if (laboratory != null)
                {
                    var laboratorySearched = MyLabService.SearchLaboratory(laboratory);
                    if (laboratorySearched.ObjectResponse != null)
                    {
                        new CreateLabWindow(this, MyLabService, laboratorySearched.ObjectResponse, labsDataGrid.SelectedIndex).ShowDialog();
                    }
                    else
                        MessageBox.Show("Laboratorio NO EXISTE", "CSA LABS",
                        MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Debes seleccionar un laboratorio del registro para poder crear/editar", "CSA LABS",
                        MessageBoxButton.OK);
                }
            }
        }

        private void trashButton_Click(object sender, RoutedEventArgs e)
        {
            if (labsDataGrid.Items.Count > 0)
            {
                var laboratory = (Laboratory)labsDataGrid.SelectedItem;
                if (laboratory != null)
                {
                    var message = MessageBox.Show("¿Estás seguro de querer eliminar este laboratorio?", "CSA LABS",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (message == MessageBoxResult.Yes) 
                    {
                        var response = MyLabService.DeleteLaboratory(laboratory);
                        if (response.ObjectResponse != null)
                        {
                            MessageBox.Show("Laboratorio eliminado correctamente", "CSA LABS",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error inesperado " + response.Message, "CSA LABS",
                                                           MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debes seleccionar un laboratorio del registro para poder eliminar", "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
