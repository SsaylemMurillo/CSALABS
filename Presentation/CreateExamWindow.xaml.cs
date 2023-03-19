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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for CreateExamWindow.xaml
    /// </summary>
    public partial class CreateExamWindow : Window
    {
        public ExamService MyExamService { get; set; }
        List<TextBox> registerTextBoxes;

        public CreateExamWindow()
        {

            InitializeComponent();
            registerTextBoxes = new List<TextBox>();
            MyExamService = new ExamService(ConnectionStringExtractor.connectionString);
            FillTextBoxesList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
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

        private void CleanFields()
        {
            foreach (TextBox item in registerTextBoxes)
            {
                item.Text = null;
            }
        }

        private void FillTextBoxesList()
        {
            registerTextBoxes.Add(idExamenTextBox);
            registerTextBoxes.Add(nameExamTextBox);
            registerTextBoxes.Add(descriptionExamTextBox);
            registerTextBoxes.Add(valueMeasuresExamTextBox);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                try
                {
                    var idValue = DataConversor.ConvertStringToInt(idExamenTextBox.Text);

                    if (idValue != -1)
                    {
                        Exam exam = new 
                            Exam(idValue, nameExamTextBox.Text, descriptionExamTextBox.Text, valueMeasuresExamTextBox.Text);

                        var response = MyExamService.SaveExam(exam);
                        MessageBox.Show(response.Message, "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Information);
                        CleanFields();
                    }
                    else
                    {
                        if (idValue == -1)
                        {
                            MessageBox.Show("El id que intentas ingresar no es válido", "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "CSA LABS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Campos Vacios Encontrados ! Revisa la información digitada.", "CSA LABS");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
