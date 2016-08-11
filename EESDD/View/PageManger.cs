using EESDD.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EESDD.View
{
    public enum PageCluster
    {
        None,
        Login
    }

    static class PageManger
    {
        static PageManger()
        {
            main = new Container();
        }

        private static Container main;

        private static LoginPage login;

        private static PageCluster current;
        public static PageCluster CurrentPage
        {
            set
            {
                if (current == value)
                    return;

                Page page;
                switch (value)
                {
                    case PageCluster.Login:
                        page = GetLoginReady();
                        break;
                    default:
                        return;
                }
                main.SetPage(page);
                current = value;
            }
        }

        private static Page GetLoginReady()
        {
            if (login == null)
            {
                login = new LoginPage();
            }

            return login;
        }

        public static void ShowMain()
        {
            main.Show();
        }
        public static void Hit() { }
    }
}
