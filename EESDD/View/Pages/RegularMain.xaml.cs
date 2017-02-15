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
using EESDD.Class.Model;

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for RegularMain.xaml
    /// </summary>
    public partial class RegularMain : Page
    {
        public RegularMain()
        {
            InitializeComponent();
        }

        public void SetPage(Regular regular)
        {
            iName.Text = regular.RealName;
            iAge.Text = regular.Age.ToString();
            iDriAge.Text = regular.DriAge.ToString();
            iLastDate.Text = regular.LastDate.ToString();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            CU.MG_Page.CurrentPage = PageCluster.RegularInfo;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            CU.MG_Page.CurrentPage = PageCluster.GameSelect;
        }
    }
}
