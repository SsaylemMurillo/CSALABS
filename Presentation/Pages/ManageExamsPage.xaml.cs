using BusinessLogicLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ManageExamsPage.xaml
    /// </summary>
    public partial class ManageExamsPage : Page
    {
        public Frame MainFrame { get; set; }
        public Page PreviousPage { get; set; }

        public ExamService MyExamService { get; set; }

        public LaboratoryService MyLabService { get; set; }

        public ManageExamsPage(Frame mainFrame, Page previousPage)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            PreviousPage = previousPage;
            MyExamService = new ExamService(ConnectionStringExtractor.connectionString);
            MyLabService = new LaboratoryService(ConnectionStringExtractor.connectionString);
            ChargeDataGrid();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainPage(MainFrame));
        }

        private void ChargeDataGrid()
        {
            var response = MyExamService.GetAll();
            if (response.DataList != null)
            {
                LoadExamsDataGrid(response.DataList);
            }
        }

        public void LoadExamsDataGrid(List<Exam> exams)
        {
            examsDataGrid.Items.Clear();

            if (exams != null)
            {
                foreach (Exam item in exams)
                {
                    examsDataGrid.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void pencilButton_Click(object sender, RoutedEventArgs e)
        {
            CreateExamWindow examWindow = new CreateExamWindow();
            examWindow.ShowDialog();

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ChargeDataGrid();
            });
        }

        private void trashButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete
            var selectedExam = (Exam)examsDataGrid.SelectedItem;
            if (selectedExam != null)
            {
                var message = MessageBox.Show("¿ Estás seguro de querer eliminar este examen ?",
                        "CSA LABS",
                        MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (message == MessageBoxResult.Yes)
                {
                    var response = MyExamService.DeleteExam(selectedExam);
                    MessageBox.Show(response.Message, "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    ChargeDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un examen del registro para poder eliminar", "CSA LABS",
                    MessageBoxButton.OK);
            }
        }
    }
}
