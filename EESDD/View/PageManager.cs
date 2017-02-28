using EESDD.Class.Control;
using EESDD.Class.Model;
using EESDD.Module.UDP;
using EESDD.View.Pages;
using EESDD.View.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ActionBinding();
            StartUDPTest();
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
        private Stack<PageCluster> last;

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
                        RegularInfo.SetPage(CU.MG_User.User as Regular);
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

                PushPage();
                current = value;
            }
        }

        private void PushPage()
        {
            if (last == null)
                last = new Stack<PageCluster>();

            if (current != PageCluster.GameRealTime)
                last.Push(current);
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
                GameSelect.ResetPage();
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

        #region Container Button
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

        private void ActionBinding()
        {
            Main.LogoutHandler += LogoutAction;
            Main.ReturnHandler += ReturnAction;
            Main.SettingHandler += SettingAction;

            Main.BeforeClosedCheckHandler += ForceShutDown;
        }

        private void StartUDPTest()
        {
            ThreadManager.DefineThread(ThreadCluster.UDPTest, UDPTest);
            ThreadManager.StartThread(ThreadCluster.UDPTest);
        }

        private void UDPTest()
        {
            while (true)
            {
                bool? link = CU.MG_UDP.TestLink();
                if (link != null)
                    Link = link;
                Thread.Sleep(1000);
            }
        }

        private bool ForceShutDown()
        {
            if (ThreadManager.IsBusy())
            {
                var result = CustomMessageBox.Show("存在任务正在运行，确定强行退出？");
                if (result != ResultType.True)
                    return false;
            }

            ThreadManager.KillAll();
            return true;
        }

        private void LogoutAction()
        {
            CU.MG_User.LogOut();
            CurrentPage = PageCluster.Login;
        }

        private void ReturnAction()
        {
            CurrentPage = last.Pop();
        }

        private void SettingAction()
        {

        }
        #endregion

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
                    CU.MG_Exp.ThreadLoad(user as Regular);
                    break;
                default:
                    break;
            }
        }
        

        #region GamePlay
        /* 
         * This runs only once to get GameRealTime Page ready 
         * and binds actions before, during and after game. 
         */
        private void GameFirstStep()
        {
            GetGameRealTimeReady();
            if (GameRealTime.FirstRun)
            {
                SetUDPRefreshAction();
                SetUDPTimeOutAction();
                SetEndAction();
            }
        }

        public void GameStartAction(Game game)
        {
            GameFirstStep();

            GameRealTime.ResetPage();
            CurrentPage = PageCluster.GameRealTime;

            /* Start the UDP receiving thread */
            CU.Player.Start(game.Scene, game.Mode);
        }

        private void SetUDPRefreshAction()
        {
            CU.Player.RefreshHandler += CU.MG_Page.GameRealTime.SetPage;
        }

        private void SetUDPTimeOutAction()
        {
            CU.MG_UDP.ReceiveTimeOutAction += () =>
            {
                if (current == PageCluster.GameRealTime)
                {
                    Application.Current.Dispatcher.BeginInvoke((System.Action)(delegate ()
                    {
                        CustomMessageBox.Show("UDP连接超时，请检查网络是否正常连通！");
                        GameEndAction();
                    }));
                }
            };
        }

        private void SetEndAction()
        {
            CU.Player.StopHandler += SaveExp;
        }

        private void SaveExp(Exp exp)
        {
            CU.MG_Exp.AddExp(exp, true);
            CU.MG_Exp.ThreadSave(CU.MG_User.User as Regular);
        }

        public void GameEndAction()
        {
            ExpType type = ExpTypeBox.Show();
            if (type != ExpType.Cancel)
            {
                CU.Player.End(type);
                CurrentPage = PageCluster.GameSelect;
            }
        }
        #endregion
    }
}
