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
            SetText();
        }

        private static ExpType result;

        private void SetText()
        {
            tNormalText.Text = CU.MG_Set.Text["exp_type"]["normal"];
            tCarCrashNormalText.Text = CU.MG_Set.Text["exp_type"]["carcrash_normal"];
            tCarCrashNormalText.Text = CU.MG_Set.Text["exp_type"]["carcrash_accident"];
            tOperatorAccidentText.Text = CU.MG_Set.Text["exp_type"]["operator_accident"];
            tOthersText.Text = CU.MG_Set.Text["exp_type"]["others"];
        }

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
                result = ExpType.CarCrash_Normal;
            if (tCarCrashAccidentButton.IsChecked == true)
                result = ExpType.CarCrash_Accident;
            if (tOperatorAccidentButton.IsChecked == true)
                result = ExpType.Operator_Accident;
            if (tOthersButton.IsChecked == true)
                result = ExpType.CarCrash_Normal;

            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void tNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tNormalButton.IsChecked = true;
        }

        private void tCarCrashNormal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tCarCrashNormalButton.IsChecked = true;
        }

        private void tCarCrashAccident_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tCarCrashAccidentButton.IsChecked = true;
        }

        private void tOperatorAccident_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tOperatorAccidentButton.IsChecked = true;
        }

        private void tOthers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tOthersButton.IsChecked = true;
        }
    }
}
