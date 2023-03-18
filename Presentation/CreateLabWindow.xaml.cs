using BusinessLogicLayer;
using Entity;
using Presentation.Pages;
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
    /// Interaction logic for CreateLabWindow.xaml
    /// </summary>
    public partial class CreateLabWindow : Window
    {
        public ManageLabPage Page { get; set; }

        List<TextBox> editTextBoxes;
        public Laboratory MyLaboratory { get; set; }
        public int SelectedRow { get; set; }
        public LaboratoryService MyLabService { get; set; }
        public ExamService MyExamService { get; set; }

        public CreateLabWindow(ManageLabPage page, LaboratoryService service, Laboratory laboratory, int rowSelected)
        {
            MyLaboratory = laboratory;
            SelectedRow = rowSelected;
            MyLabService = service;
            MyExamService = new ExamService(ConnectionStringExtractor.connectionString);
            InitializeComponent();
            selectedRowTextBlock.Text = "" + (SelectedRow + 1);
            editTextBoxes = new List<TextBox>();
            LoadFields();
            LoadFieldsValues();
            Page = page;
        }

        private void LoadFields()
        {
            editTextBoxes.Add(labPlaceTextBox);
            editTextBoxes.Add(addExamTextBox);
        }

        private void LoadFieldsValues()
        {
            if (MyLaboratory != null)
            {
                labPlaceTextBox.Text = MyLaboratory.Place;
                LoadExamsTable();
            }
        }

        private void LoadExamsTable()
        {
            examsDataGrid.Items.Clear();
            var response = MyExamService.GetAllExamLaboratory(MyLaboratory.Id);

            if (response.DataList != null)
            {
                MyLaboratory.Exams = response.DataList;
                foreach (var item in response.DataList)
                {
                    examsDataGrid.Items.Add(item);
                }
            }
        }

        private void UnSavedLoadExamsTable()
        {
            examsDataGrid.Items.Clear();

            foreach (var item in MyLaboratory.Exams)
            {
                examsDataGrid.Items.Add(item);
            }
        }

        public CreateLabWindow()
        {
            InitializeComponent();
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

        private void addExamTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Enter key is pressed
                if (!string.IsNullOrEmpty(addExamTextBox.Text) && addExamTextBox.Text.Length > 0)
                {
                    // Search exam in database
                    var idValue = DataConversor.ConvertStringToInt(addExamTextBox.Text);
                    if (idValue != -1)
                    {
                        var response = MyExamService.SearchExam(new Exam(idValue));
                        if (response.ObjectResponse != null)
                        {
                            // Add exam to a lab
                            MyLaboratory.Exams.Add(response.ObjectResponse);

                            UnSavedLoadExamsTable();
                        }
                        else
                        {
                            MessageBox.Show("El ID de examen ingresado no es reconocible", "CSA LABS",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        addExamTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("El ID de examen ingresado no está en formato correcto", "CSA LABS",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debes ingresar un id de examen para agregar", "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show("¿Estás seguro de querer guardar los cambios en este laboratorio?", "CSA LABS",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (message == MessageBoxResult.Yes)
            {
                if (!string.IsNullOrEmpty(labPlaceTextBox.Text) && labPlaceTextBox.Text.Length > 0)
                {
                    MyLaboratory.Place = labPlaceTextBox.Text;
                    var response = MyLabService.UpdateLaboratory(MyLaboratory);
                    MessageBox.Show(response.Message, "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("El Campo de lugar laboratorio no puede estar vacío", "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void trashButton_Click(object sender, RoutedEventArgs e)
        {

            // Edition Window: Pencil On Right Side Bar
            var myExam = (Exam)examsDataGrid.SelectedItem;
            if (myExam != null)
            {
                var message = MessageBox.Show("¿Estás seguro de querer eliminar este examen?", "CSA LABS",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (message == MessageBoxResult.Yes)
                {
                    var response = MyLabService.DeleteExamsFromLaboratory(MyLaboratory, myExam);
                    MessageBox.Show(response.Message, "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El Campo de lugar laboratorio no puede estar vacío", "CSA LABS",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un paciente del registro para poder editar", "CSA LABS",
                    MessageBoxButton.OK);
            }
        }
    }
}
