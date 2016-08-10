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
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public ImageButton()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageButton));
        public static readonly DependencyProperty HoverImageProperty =
            DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(ImageButton));

        public ImageSource NormalImage
        {
            get { return imgNormal.ImageSource; }
            set { imgNormal.ImageSource = value; }
        }

        public ImageSource HoverImage
        {
            get { return imgHover.ImageSource; }
            set { imgHover.ImageSource = value; }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            NormalButton.Visibility = System.Windows.Visibility.Hidden;
            HoverButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            NormalButton.Visibility = System.Windows.Visibility.Visible;
            HoverButton.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
