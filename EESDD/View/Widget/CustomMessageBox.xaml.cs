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
    public enum ResultType
    {
        True,
        False,
        Cancel
    }
    public partial class CustomMessageBox : Window
    {

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private static ResultType Result;

        public new string Title
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }

        public string Message
        {
            get { return this.lblMsg.Text; }
            set { this.lblMsg.Text = value; }
        }

        public static ResultType Show(string msg, string title)
        {
            var msgBox = new CustomMessageBox();
            msgBox.Title = title;
            msgBox.Message = msg;
            msgBox.ShowDialog();
            return Result;
        }

        public static ResultType Show(string msg)
        {
            return Show(msg, "消息提示");
        }

        private void True_Click(object sender, MouseButtonEventArgs e)
        {
            Result = ResultType.True;
            this.Close();
        }

        private void False_Click(object sender, MouseButtonEventArgs e)
        {
            Result = ResultType.False;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = ResultType.Cancel;
            this.Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Result = ResultType.Cancel;
            this.Close();
        }

    }
}
