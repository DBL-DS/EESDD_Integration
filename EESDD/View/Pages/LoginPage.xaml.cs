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
using System.Windows.Media.Animation;
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
            LoadAnimation();
        }

        #region Animation
        private Storyboard LeftFadeOut;
        private Storyboard RightFadeOut;
        private Storyboard FadeIn;
        private Storyboard FadeOut;
        private Storyboard ButtonFadeIn;
        private Storyboard ButtonFadeOut;

        private void LoadAnimation()
        {
            LeftFadeOut = (this.FindResource("LeftFadeOut") as Storyboard);
            RightFadeOut = (this.FindResource("RightFadeOut") as Storyboard);
            FadeIn = (this.FindResource("FadeIn") as Storyboard);
            FadeOut = (this.FindResource("FadeOut") as Storyboard);
            ButtonFadeIn = (this.FindResource("ButtonFadeIn") as Storyboard);
            ButtonFadeOut = (this.FindResource("ButtonFadeOut") as Storyboard);
        }
        # endregion

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
            Storyboard.SetTarget(LeftFadeOut, LoginPanel);
            LeftFadeOut.Begin();

            Storyboard.SetTarget(ButtonFadeOut, RightButton);
            ButtonFadeOut.Begin();

            Storyboard.SetTarget(FadeIn, RegisterPanel);
            FadeIn.Begin();

            Storyboard.SetTarget(ButtonFadeIn, LeftButton);
            ButtonFadeIn.Begin();
        }

        private void ShowLoginPanel()
        {
            Storyboard.SetTarget(RightFadeOut, RegisterPanel);
            RightFadeOut.Begin();

            Storyboard.SetTarget(ButtonFadeOut, LeftButton);
            ButtonFadeOut.Begin();

            Storyboard.SetTarget(FadeIn, LoginPanel);
            FadeIn.Begin();

            Storyboard.SetTarget(ButtonFadeIn, RightButton);
            ButtonFadeIn.Begin();
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
