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
using System.Windows.Shapes;

namespace EESDD.View.Widget
{
    /// <summary>
    /// Interaction logic for ExpTypeBox.xaml
    /// </summary>
    public partial class ExpTypeBox : Window
    {
        public ExpTypeBox()
        {
            InitializeComponent();
        }

        private static ExpType result;

        public static new ExpType Show()
        {
            var typeBox = new ExpTypeBox();
            typeBox.ShowDialog();
            return result;
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            result = ExpType.Cancel;
            this.Close();
        }

        private void True_Click(object sender, MouseButtonEventArgs e)
        {
            if (tNormalButton.IsChecked == true)
                result = ExpType.Normal;
            if (tCarCrashNormalButton.IsChecked == true)
                result = ExpType.CarCrashNormal;
            if (tCarCrashAccidentButton.IsChecked == true)
                result = ExpType.CarCrashAccident;
            if (tOperatorAccidentButton.IsChecked == true)
                result = ExpType.OperatorAccident;
            if (tOtherButton.IsChecked == true)
                result = ExpType.CarCrashNormal;

            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
