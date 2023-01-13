using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for ModuleButtonController.xaml
    /// </summary>
    public partial class ModuleButtonController : UserControl
    {
        public Frame MainFrame { get; set; }
        public string moduleName { get; set; }
        public Page moduleDirection { get; set; }
        public string buttonColor { get; set; }
        public string buttonColorVariation { get; set; }
        public string buttonImageContent { get; set; }

        public ModuleButtonController(Frame mainFrame, Page pageDirection, string moduleName, string buttonColor, string buttonColorVariation, string buttonImageContent)
        {
            InitializeComponent();
            this.MainFrame = mainFrame;
            this.moduleDirection = pageDirection;
            this.moduleName = moduleName;
            this.buttonColor = buttonColor;
            this.buttonColorVariation = buttonColorVariation;
            this.buttonImageContent = buttonImageContent;
            InitializeData();
        }

        private void InitializeData()
        {
            moduleNumberLabel.Content = moduleName;
            buttonImage.Source = ChangeImageOfButton(buttonImageContent);
            buttonModule.Background = ColorChangerInHex(buttonColor);
        }

        private void buttonModule_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = moduleDirection;
        }

        public BitmapImage ChangeImageOfButton(string path)
        {
            return new BitmapImage(ResourceAccessor.Get(path));
        }

        public SolidColorBrush ColorChangerInHex(string hex)
        {
           return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        }

        internal static class ResourceAccessor
        {
            public static Uri Get(string resourcePath)
            {
                var uri = string.Format(
                    "pack://application:,,,/{0};component/{1}"
                    , Assembly.GetExecutingAssembly().GetName().Name
                    , resourcePath
                );

                return new Uri(uri);
            }
        }

        private void buttonModule_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonModule.Background = ColorChangerInHex(buttonColorVariation);
        }

        private void buttonModule_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonModule.Background = ColorChangerInHex(buttonColor);
        }
    }
}
