using EESDD.Class.Model;
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
        private PageCluster last;
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
                SetLogoutButton(value);
                SetReturnButton(value);
                SetSettingButton(value);
                Main.SetPage(page);
                last = current;
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

        public string Info
        {
            set
            {
                if (value == null)
                    Main.ResetInfo();
                else
                    Main.SetInfo(value);
            }
        }

        public string Name
        {
            set
            {
                if (value == null)
                    Main.ResetName();
                else
                    Main.SetName(value);
            }
        }

        public bool? Link
        {
            set
            {
                if (value == null)
                    Main.ResetLink();
                else
                    Main.SetLink((bool)value);
            }
        }

        public void SetReturnButton(PageCluster page)
        {
            switch (page)
            {
                case PageCluster.GameSelect:
                case PageCluster.GameRealTime:
                case PageCluster.GameData:
                case PageCluster.RegularInfo:
                case PageCluster.GameSetting:
                case PageCluster.UserManage:
                case PageCluster.AdminInfo:
                case PageCluster.AdminGrant:
                    Main.ShowReturnButton();
                    break;
                default:
                    Main.HideReturnButton();
                    break;
            }
        }

        public void SetLogoutButton(PageCluster page)
        {
            switch (page)
            {
                case PageCluster.RegularMain:
                case PageCluster.GameSelect:
                case PageCluster.GameRealTime:
                case PageCluster.GameData:
                case PageCluster.RegularInfo:
                case PageCluster.AdminMain:
                case PageCluster.GameSetting:
                case PageCluster.UserManage:
                case PageCluster.AdminInfo:
                case PageCluster.AdminGrant:
                    Main.ShowLogoutButton();
                    break;
                default:
                    Main.HideLogoutButton();
                    break;
            }
        }

        public void SetSettingButton(PageCluster page)
        {
            switch (page)
            {
                case PageCluster.Login:
                case PageCluster.RegularMain:
                case PageCluster.GameSelect:
                case PageCluster.GameRealTime:
                case PageCluster.GameData:
                case PageCluster.RegularInfo:
                case PageCluster.AdminMain:
                case PageCluster.GameSetting:
                case PageCluster.UserManage:
                case PageCluster.AdminInfo:
                case PageCluster.AdminGrant:
                    Main.ShowSettingButton();
                    break;
                default:
                    Main.HideSettingButton();
                    break;
            }
        }

        public void LoginAction(User user)
        {
            Name = user.Name;
            switch (user.Group)
            {
                case UserGroup.ADMIN:
                    CurrentPage = PageCluster.AdminMain;
                    break;
                case UserGroup.REGULAR:
                    CurrentPage = PageCluster.RegularMain;
                    RegularMain.SetPage(user as Regular);
                    break;
                default:
                    break;
            }
        }


    }
}
