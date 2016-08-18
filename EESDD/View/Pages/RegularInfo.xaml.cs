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
        }

        private void InfoSaveButton_Click(object sender, RoutedEventArgs e)
        {
            InfoEditButton.Visibility = System.Windows.Visibility.Visible;
            InfoSaveButton.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
