﻿using EESDD.View.Pages;
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
        Login,
        RegularMain
    }

    class PageManager
    {
        public PageManager()
        {
            main = new Container();
        }

        private Container main;

        private LoginPage login;
        private RegularMain regularMain;

        private PageCluster current;
        public PageCluster CurrentPage
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
                    case PageCluster.RegularMain:
                        page = GetRegularMainReady();
                        break;
                    default:
                        return;
                }
                main.SetPage(page);
                current = value;
            }
        }

        private Page GetLoginReady()
        {
            if (login == null)
            {
                login = new LoginPage();
            }

            return login;
        }

        private Page GetRegularMainReady()
        {
            if (regularMain == null)
                regularMain = new RegularMain();

            return regularMain;
        }

        public void ShowMain()
        {
            main.Show();
        }
    }
}