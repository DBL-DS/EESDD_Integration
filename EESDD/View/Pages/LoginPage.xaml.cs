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

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ShowRegularButton()
        {
            RegularButton.Visibility = System.Windows.Visibility.Visible;
            AdminButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ShowAdminButton()
        {
            RegularButton.Visibility = System.Windows.Visibility.Collapsed;
            AdminButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void ShowRegisterPanel()
        {
            LeftButton.Visibility = System.Windows.Visibility.Visible;
            RightButton.Visibility = System.Windows.Visibility.Hidden;
            LoginPanel.Visibility = System.Windows.Visibility.Hidden;
            RegisterPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ShowLoginPanel()
        {
            LeftButton.Visibility = System.Windows.Visibility.Hidden;
            RightButton.Visibility = System.Windows.Visibility.Visible;
            LoginPanel.Visibility = System.Windows.Visibility.Visible;
            RegisterPanel.Visibility = System.Windows.Visibility.Hidden;
        }

        # region Event
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRegisterPanel();
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginPanel();
        }

        private void RegularButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAdminButton();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRegularButton();
        }
        # endregion
    }
}
