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

namespace EESDD.View.Widget
{
    /// <summary>
    /// Interaction logic for ModeButton.xaml
    /// </summary>
    public partial class ModeButton : UserControl
    {
        public ModeButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ModeImageProperty =
            DependencyProperty.Register("ModeImage", typeof(ImageSource), typeof(ModeButton));
        public static readonly DependencyProperty ModeTextProperty =
            DependencyProperty.Register("ModeText", typeof(string), typeof(ModeButton));
        public static readonly DependencyProperty CheckProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ModeButton));


        public ImageSource ModeImage
        {
            get { return mImage.Source; }
            set { mImage.Source = value; }
        }

        public string ModeText
        {
            get { return mText.Text; }
            set { mText.Text = value; }
        }
       
    }
}
