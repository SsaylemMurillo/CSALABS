using Presentation.UserControls;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<ModuleButtonController> modules;
        Frame mainFrame;

        public MainPage(Frame mainFrame)
        {
            InitializeComponent();
            modules = new List<ModuleButtonController>();
            this.mainFrame = mainFrame;
            FillModules();
            SendModulesToDataGrid();
        }

        public void FillModules()
        {
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Registro de Pacientes", "#674ea7", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new ManagePatientPage(mainFrame, this), "Gestor de Pacientes", "#5aef6a", "#e91d63", "/Images/edit.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new ManageLabPage(mainFrame, this), "Gestor de Laboratorios", "#f76f81", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #2", "#d2be77", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #3", "#4400bf", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #4", "#fa7603", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #5", "#64cf06", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #6", "#c58fa6", "#e91d63", "/Images/add_user.png"));
            modules.Add(new ModuleButtonController(mainFrame,
                new RegisterPatientPage(mainFrame, this), "Modulo #7", "#add344", "#e91d63", "/Images/add_user.png"));
        }

        public void SendModulesToDataGrid()
        {
            int columnIterator = 0;
            int rowIterator = 0;

            modulesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            modulesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            modulesGrid.ColumnDefinitions.Add(new ColumnDefinition());
            modulesGrid.RowDefinitions.Add(new RowDefinition());
            modulesGrid.RowDefinitions.Add(new RowDefinition());
            modulesGrid.RowDefinitions.Add(new RowDefinition());

            foreach (var item in modules)
            {
                Grid.SetColumn(item, columnIterator);
                Grid.SetRow(item, rowIterator);

                modulesGrid.Children.Add(item);

                if (columnIterator < 2)
                {
                    columnIterator++;
                }
                else
                {
                    rowIterator++;
                    columnIterator = 0;
                }
            }
        }
    }
}
