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
    /// Interaction logic for ButtonMenuController.xaml
    /// </summary>
    public partial class ButtonMenuController : UserControl
    {
        public string MyButtonImage { get; set; }
        public string MyButtonText { get; set; }
        public Page PageOfButton { get; set; }
        public Frame MainFrame { get; set; }
        public TextBlock TextFrame { get; set; }

        public ButtonMenuController()
        {
            InitializeComponent();
        }

        public void ChangeImageOfButton(string sourceImage)
        {
            MyButtonImage = sourceImage;
            imageMenu.Source = new BitmapImage(ResourceAccessor.Get(sourceImage));
        }

        public void ChangeTextOfButton(string sourceText)
        {
            myButtonText.Text = sourceText;
        }

        private void buttonMenu_Click(object sender, RoutedEventArgs e)
        {
            // Change Button Color -> Blue
            // Change Icon Color -> White

            MainFrame.Content = PageOfButton;
            TextFrame.Text = myButtonText.Text;
            buttonMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D4E0FC"));
            myButtonText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        }
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
}
