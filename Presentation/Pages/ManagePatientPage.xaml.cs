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
    /// Interaction logic for ManagePatientPage.xaml
    /// </summary>
    public partial class ManagePatientPage : Page
    {
        public Frame MainFrame { get; set; }
        public Page PreviousPage { get; set; }

        public int FilterValue { get; set; }
        List<TextBox> filterTextBoxes;
        public PatientService MyPatientService { get; set; }

        public ManagePatientPage(Frame mainFrame, Page previousPage)
        {
            filterTextBoxes = new List<TextBox>();
            MyPatientService = new PatientService(ConnectionStringExtractor.connectionString);
            InitializeComponent();
            LoadPatientDataGrid();
            LoadComboBox();
            FillTextBoxesList();
            FilterValue = 0;
            MainFrame = mainFrame;
            PreviousPage = previousPage;
        }

        private void FillTextBoxesList()
        {
            filterTextBoxes.Add(idFilterTextBox);
            filterTextBoxes.Add(idTypeFilterTextBox);
            filterTextBoxes.Add(firstNameFilterTextBox);
            filterTextBoxes.Add(addressFilterTextBox);
            filterTextBoxes.Add(bornDateFilterTextBox);
        }

        private int LoadComboBox()
        {
            int value = 0;
            if (resultsByPageComboBox.SelectedItem!= null)
            {
                if (resultsByPageComboBox.SelectedIndex == 0)
                {
                    value = 20;
                }
                else if (resultsByPageComboBox.SelectedIndex == 1)
                {
                    value = 40;
                }
                else if (resultsByPageComboBox.SelectedIndex == 2)
                {
                    value = 60;
                }
                else if (resultsByPageComboBox.SelectedIndex == 3)
                {
                    value = 80;
                }
                else if (resultsByPageComboBox.SelectedIndex == 4)
                {
                    value = 100;
                }
            }
            registerNumberTextBlock.Text = "" + value;
            return value;
        }

        public void LoadPatientDataGrid()
        {
            patientsDataGrid.Items.Clear();
            var response = MyPatientService.GetAll();

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadPatientDataGridRange(int value)
        {
            patientsDataGrid.Items.Clear();
            var response = MyPatientService.GetAll();

            if (response.DataList != null)
            {
                int i = 0;
                registersTextBlock.Text = response.DataList.Count.ToString();
                foreach (Patient item in response.DataList)
                {
                    if (i < value)
                    {
                        patientsDataGrid.Items.Add(item);
                        i++;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado",
                    "CSA LABS",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Edition Window: Pencil On Right Side Bar
            var myPatient = (Patient)patientsDataGrid.SelectedItem;     
            if (myPatient != null)
            {
                new EditPatientWindow(this, MyPatientService, myPatient, patientsDataGrid.SelectedIndex).ShowDialog();
            }
            else
            {
                MessageBox.Show("Debes seleccionar un paciente del registro para poder editar", "CSA LABS",
                    MessageBoxButton.OK);
            }
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            // Filter Window: Filter On Right Side Bar
            editPanel.Visibility = Visibility.Visible;
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            // < Atras: Table 
        }

        private void Border_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            // 1: Table
        }

        private void Border_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            // 2: Table
        }

        private void Border_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            // 3: Table
        }

        private void Border_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            // 4: Table
        }

        private void resultsByPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = LoadComboBox();
            LoadPatientDataGridRange(value);
        }

        private void Border_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            editPanel.Visibility = Visibility.Collapsed;
        }

        private void txtFilterSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void textSearchFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void idFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = MyPatientService.GetAllFilterId(idFilterTextBox.Text);

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                patientsDataGrid.Items.Clear();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                patientsDataGrid.Items.Clear();
            }
        }

        private void idTypeFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = MyPatientService.GetAllFilterIdType(idTypeFilterTextBox.Text);

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                patientsDataGrid.Items.Clear();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                patientsDataGrid.Items.Clear();
            }
        }

        private void firstNameFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = MyPatientService.GetAllFilterFirstName(firstNameFilterTextBox.Text);

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                patientsDataGrid.Items.Clear();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                patientsDataGrid.Items.Clear();
            }
        }

        private void addressFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = MyPatientService.GetAllFilterAddress(addressFilterTextBox.Text);

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                patientsDataGrid.Items.Clear();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                patientsDataGrid.Items.Clear();
            }
        }

        private void bornDateFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var response = MyPatientService.GetAllFilterBornDate(bornDateFilterTextBox.Text);

            if (response.DataList != null)
            {
                registersTextBlock.Text = response.DataList.Count.ToString();
                patientsDataGrid.Items.Clear();
                foreach (Patient item in response.DataList)
                {
                    patientsDataGrid.Items.Add(item);
                }
            }
            else
            {
                patientsDataGrid.Items.Clear();
            }

        }

        private bool CheckTextBoxStringValue(TextBox textBox)
        {
            bool value = true;
            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.Length > 0)
            {
                FilterValue++;
            }
            else if (FilterValue != 0)
            {
                FilterValue--;
                value = false;
            }
            UpdateFilterTextValue();
            return value;
        }

        private void UpdateFilterTextValue()
        {
            filtersTextBlock.Text = "" + FilterValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilterValue = 0;
            UpdateFilterTextValue();
            // Clean Fields

            foreach (TextBox item in filterTextBoxes)
            {
                item.Text = null;
            }
        }

        private void idFilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextBoxStringValue(idFilterTextBox);
        }

        private void idTypeFilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextBoxStringValue(idTypeFilterTextBox);
        }

        private void firstNameFilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextBoxStringValue(firstNameFilterTextBox);
        }

        private void addressFilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextBoxStringValue(addressFilterTextBox);
        }

        private void bornDateFilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextBoxStringValue(bornDateFilterTextBox);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new MainPage(MainFrame));
        }
    }
}
