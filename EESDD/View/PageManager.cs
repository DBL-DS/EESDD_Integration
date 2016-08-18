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

        Login,

        RegularMain,
        GameSelect,
        GameRealTime,
        GameData,
        RegularInfo,

        AdminMain,
        GameSetting,
        UserManage,
        AdminInfo,
        AdminGrant
    }

    class PageManager
    {
        public PageManager()
        {
            Main = new Container();
        }

        private Container Main;

        private Login Login;

        private RegularMain RegularMain;
        private GameSelect GameSelect;
        private GameRealTime GameRealTime;
        private GameData GameData;
        private RegularInfo RegularInfo;

        private AdminMain AdminMain;
        private GameSetting GameSetting;
        private UserManage UserManage;
        private AdminInfo AdminInfo;
        private AdminGrant AdminGrant;

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
                    case PageCluster.None:
                        page = null;
                        break;
                    case PageCluster.Login:
                        page = GetLoginReady();
                        break;
                    case PageCluster.RegularMain:
                        page = GetRegularMainReady();
                        break;
                    case PageCluster.GameSelect:
                        page = GetGameSelectReady();
                        break;
                    case PageCluster.GameRealTime:
                        page = GetGameRealTimeReady();
                        break;
                    case PageCluster.GameData:
                        page = GetGameDataReady();
                        break;
                    case PageCluster.RegularInfo:
                        page = GetRegularInfoReady();
                        break;
                    case PageCluster.AdminMain:
                        page = GetAdminMainReady();
                        break;
                    case PageCluster.GameSetting:
                        page = GetGameSettingReady();
                        break;
                    case PageCluster.UserManage:
                        page = GetUserManageReady();
                        break;
                    case PageCluster.AdminInfo:
                        page = GetAdminInfoReady();
                        break;
                    case PageCluster.AdminGrant:
                        page = GetAdminGrantReady();
                        break;
                    default:
                        page = null;
                        break;
                }
                Main.SetPage(page);
                current = value;
            }
        }

        # region GetPageReady
        private Page GetLoginReady()
        {
            if (Login == null)
            {
                Login = new Login();
            }

            return Login;
        }

        private Page GetRegularMainReady()
        {
            if (RegularMain == null)
            {
                RegularMain = new RegularMain();
            }

            return RegularMain;
        }

        private Page GetGameSelectReady()
        {
            if (GameSelect == null)
            {
                GameSelect = new GameSelect();
            }

            return GameSelect;
        }

        private Page GetGameRealTimeReady()
        {
            if (GameRealTime == null)
            {
                GameRealTime = new GameRealTime();
            }

            return GameRealTime;
        }

        private Page GetGameDataReady()
        {
            if (GameData == null)
            {
                GameData = new GameData();
            }

            return GameData;
        }

        private Page GetRegularInfoReady()
        {
            if (RegularInfo == null)
            {
                RegularInfo = new RegularInfo();
            }

            return RegularInfo;
        }

        private Page GetAdminMainReady()
        {
            if (AdminMain == null)
            {
                AdminMain = new AdminMain();
            }

            return AdminMain;
        }

        private Page GetGameSettingReady()
        {
            if (GameSetting == null)
            {
                GameSetting = new GameSetting();
            }

            return GameSetting;
        }

        private Page GetUserManageReady()
        {
            if (UserManage == null)
            {
                UserManage = new UserManage();
            }

            return UserManage;
        }

        private Page GetAdminInfoReady()
        {
            if (AdminInfo == null)
            {
                AdminInfo = new AdminInfo();
            }

            return AdminInfo;
        }

        private Page GetAdminGrantReady()
        {
            if (AdminGrant == null)
            {
                AdminGrant = new AdminGrant();
            }

            return AdminGrant;
        }
        # endregion

        public void ShowMain()
        {
            Main.Show();
        }
    }
}
