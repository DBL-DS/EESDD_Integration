using EESDD.View.Widget;
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

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Color a = Color.FromArgb(0, 0, 0, 0);
        public Container()
        {
            InitializeComponent();
        }

        public void LoadText()
        {

        }

        public void LoadEvent()
        {

        }

        public void CloseApp()
        {
            var result = CustomMessageBox.Show("确定退出？");
            if (result == ResultType.True)
                this.Close();
        }

        #region WindowEvent
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CloseApp();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Compress_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch(this.WindowState)
            {
                case System.Windows.WindowState.Maximized:
                    Compress_Button.Visibility
                        = System.Windows.Visibility.Visible;
                    ExpandButton.Visibility
                        = System.Windows.Visibility.Collapsed;
                    break;
                case System.Windows.WindowState.Normal:
                    Compress_Button.Visibility
                        = System.Windows.Visibility.Collapsed;
                    ExpandButton.Visibility
                        = System.Windows.Visibility.Visible;
                    break;
            }

        }
        # endregion

    }
}
