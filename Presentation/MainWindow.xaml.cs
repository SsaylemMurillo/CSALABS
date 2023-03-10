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

namespace Presentation
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "";
            passwordBox.Password = "";
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                ShowLoading();

                // Login Task
                Task doVerification = new Task(LoginVerification);
                doVerification.Start();
                await doVerification;
                //

                /* ERROR IN LOGIN
                MessageBox.Show("TODO: Login Verification...", "CS LABS");
                */
                new HomeScreen().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Completa la información de inicio de sesión!", "CS LABS");
            }
        }

        public void LoginVerification()
        {
            Thread.Sleep(2000);
        }

        private void ShowLoading()
        {
            if (mainPanel.Children.Count > 0)
            {
                int childrenNumber = mainPanel.Children.Count;
                for (int i = 1; i < childrenNumber; i++)
                {
                    mainPanel.Children.RemoveAt(1);
                }
            }
            gridLoading.Visibility = Visibility.Visible;
        }


        private bool ValidateFields()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0 
                && !string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                result = true;
            }
            return result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("TODO: Recover Password...", "CS LABS");
        }
    }
}
