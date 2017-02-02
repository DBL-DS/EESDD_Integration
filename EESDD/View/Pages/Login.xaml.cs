﻿using System;
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
using EESDD.Class.Model;
using EESDD.Class.Control;

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
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
            LeftFadeOut     = (this.FindResource("LeftFadeOut") as Storyboard);
            RightFadeOut    = (this.FindResource("RightFadeOut") as Storyboard);
            FadeIn          = (this.FindResource("FadeIn") as Storyboard);
            FadeOut         = (this.FindResource("FadeOut") as Storyboard);
            ButtonFadeIn    = (this.FindResource("ButtonFadeIn") as Storyboard);
            ButtonFadeOut   = (this.FindResource("ButtonFadeOut") as Storyboard);
        }
        # endregion

        private void ShowRegularButton()
        {
            RegularButton.Visibility = Visibility.Visible;
            AdminButton.Visibility = Visibility.Collapsed;
        }

        private void ShowAdminButton()
        {
            RegularButton.Visibility = Visibility.Collapsed;
            AdminButton.Visibility = Visibility.Visible;
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

        private RegisterState validateRegister()
        {
            CU.MG_User.RegisterStart(UserGroup.REGULAR);
            CU.MG_User.RegisterAdd(UserVariable.Name, rName.Text);
            CU.MG_User.RegisterAdd(UserVariable.Password, rPassword.Password);
            CU.MG_User.RegisterAdd(UserVariable.RealName, rRealName.Text);
            CU.MG_User.RegisterAdd(UserVariable.Gender,
                (bool)rFemale.IsChecked ? "Female" : "Male");
            CU.MG_User.RegisterAdd(UserVariable.Age, rAge.Text);
            CU.MG_User.RegisterAdd(UserVariable.Height, rHeight.Text);
            CU.MG_User.RegisterAdd(UserVariable.Weight, rWeight.Text);
            CU.MG_User.RegisterAdd(UserVariable.DriAge, rDriAge.Text);
            CU.MG_User.RegisterAdd(UserVariable.Career, rCareer.Text);
            CU.MG_User.RegisterAdd(UserVariable.Contact, rContact.Text);

            return CU.MG_User.RegisterEnd();
        }

        private void DisposeInfo(RegisterState state)
        {
            ClearRegisterInfo();
            ClearRegisterError();
            switch (state)
            {
                case RegisterState.USEREXIST:
                    rNameError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["user_exist"];
                    break;
                case RegisterState.DBFAILED:
                    rInfo.Text = CU.MG_Set.Text["register_state"]["db_failed"];
                    break;
                case RegisterState.VALIDATED:
                    rInfo.Text = CU.MG_Set.Text["register_state"]["validated"];
                    break;
                case RegisterState.GRANTUSERNOTEXIST:
                    // TODO
                    break;
                case RegisterState.NAMEEMPTY:
                    rNameError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["name_empty"];
                    break;
                case RegisterState.PASSWORDEMPTY:
                    rPasswordError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["password_empty"];
                    break;
                case RegisterState.REALNAMEEMPTY:
                    rRealnameError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["realname_empty"];
                    break;
                case RegisterState.GENDEREMPTY:
                    rInfo.Text = CU.MG_Set.Text["register_state"]["gender_empty"];
                    break;
                case RegisterState.HEIGHTEMPTY:
                    rHeightError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["height_empty"];
                    break;
                case RegisterState.WEIGHTEMPTY:
                    rWeightError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["weight_empty"];
                    break;
                case RegisterState.AGEEMPTY:
                    rAgeError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["age_empty"];
                    break;
                case RegisterState.DRIAGEEMPTY:
                    rDriAgeError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["driage_empty"];
                    break;
                case RegisterState.CAREEREMPTY:
                    rCareerError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["career_empty"];
                    break;
                case RegisterState.CONTACTEMPTY:
                    rContactError.Visibility = Visibility.Visible;
                    rInfo.Text = CU.MG_Set.Text["register_state"]["contact_empty"];
                    break;
                default:
                    break;
            }
        }

        private void ClearRegisterError()
        {
            rNameError.Visibility = Visibility.Hidden;
            rPasswordError.Visibility = Visibility.Hidden;
            rRealnameError.Visibility = Visibility.Hidden;
            rAgeError.Visibility = Visibility.Hidden;
            rHeightError.Visibility = Visibility.Hidden;
            rWeightError.Visibility = Visibility.Hidden;
            rDriAgeError.Visibility = Visibility.Hidden;
            rCareerError.Visibility = Visibility.Hidden;
            rContactError.Visibility = Visibility.Hidden;
        }

        private void ClearRegisterInfo()
        {
            rInfo.Text = "";
        }

        private void ClearRegisterTable()
        {
            rName.Text = "";
            rPassword.Password = "";
            rRealName.Text = "";
            rMale.IsChecked = true;
            rAge.Text = "";
            rHeight.Text = "";
            rWeight.Text = "";
            rDriAge.Text = "";
            rCareer.Text = "";
            rContact.Text = "";
        }

        private void ResetRegister()
        {
            ClearRegisterError();
            ClearRegisterInfo();
            ClearRegisterTable();
        }

        private void LoginAction(string username, string password, UserGroup group)
        {
            switch (group)
            {
                case UserGroup.ADMIN:
                    break;
                case UserGroup.REGULAR:
                    CU.MG_User.Login(rName.Text, rPassword.Password, UserGroup.REGULAR);
                    break;
                default:
                    break;
            }
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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterState state = validateRegister();
            DisposeInfo(state);
            if (state == RegisterState.SUCCESS)
                LoginAction(rName.Text, rPassword.Password, UserGroup.REGULAR);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
