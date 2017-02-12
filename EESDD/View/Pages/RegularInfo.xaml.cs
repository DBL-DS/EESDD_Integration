using EESDD.Class.Model;
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
    /// Interaction logic for RegularInfo.xaml
    /// </summary>
    public partial class RegularInfo : Page
    {
        public RegularInfo()
        {
            InitializeComponent();
        }

        private void InfoEditButton_Click(object sender, RoutedEventArgs e)
        {
            InfoEditButton.Visibility = System.Windows.Visibility.Hidden;
            InfoSaveButton.Visibility = System.Windows.Visibility.Visible;
            SetEditable();
        }

        private void InfoSaveButton_Click(object sender, RoutedEventArgs e)
        {
            InfoEditButton.Visibility = System.Windows.Visibility.Visible;
            InfoSaveButton.Visibility = System.Windows.Visibility.Hidden;
            SetUneditable();
        }

        public void SetPage(Regular regular)
        {
            iName.Text = regular.Name;
            iRealName.Text = regular.RealName;
            iMale.IsChecked = regular.Gender == "Male" ? true : false;
            iAge.Text = regular.Age.ToString();
            iHeight.Text = regular.Height.ToString();
            iWeight.Text = regular.Weight.ToString();
            iDriAge.Text = regular.DriAge.ToString();
            iCareer.Text = regular.Career;
            iContact.Text = regular.Contact;
            SetUneditable();
        }

        private void SetEditable()
        {
            iName.IsEnabled = true;
            iRealName.IsEnabled = true;
            iMale.IsEnabled = true;
            iFemale.IsEnabled = true;
            iAge.IsEnabled = true;
            iHeight.IsEnabled = true;
            iWeight.IsEnabled = true;
            iDriAge.IsEnabled = true;
            iCareer.IsEnabled = true;
            iContact.IsEnabled = true;
        }

        private void SetUneditable()
        {
            iName.IsEnabled = false;
            iRealName.IsEnabled = false;
            iMale.IsEnabled = false;
            iFemale.IsEnabled = false;
            iAge.IsEnabled = false;
            iHeight.IsEnabled = false;
            iWeight.IsEnabled = false;
            iDriAge.IsEnabled = false;
            iCareer.IsEnabled = false;
            iContact.IsEnabled = false;
        }

    }
}
