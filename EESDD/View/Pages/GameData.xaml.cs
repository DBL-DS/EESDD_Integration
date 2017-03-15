using EESDD.Class.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EESDD.View.Pages
{
    /// <summary>
    /// Interaction logic for GameData.xaml
    /// </summary>
    public partial class GameData : Page
    {
        public GameData()
        {
            InitializeComponent();
        }

        private int ExpNum;
        private List<Exp> CheckedExp;

        public void LoadExpList()
        {
            ClearExpList();
            if (CU.MG_User.User.Group == UserGroup.REGULAR &&
                CU.MG_Exp.Exps != null)
            {
                foreach (var exp in CU.MG_Exp.Exps)
                {
                    AddExpCell(exp);
                }
            }
        }

        public void ClearExpList()
        {
            ExpNum = 0;
            CheckedExp = new List<Exp>();
            ExpListPanel.Children.Clear();
        }

        public void AddExpCell(Exp exp)
        {
            var expCell = new ExpCell(++ExpNum ,exp);
            expCell.Checked += (sender, e) => { CheckAction(exp); };
            expCell.Unchecked += (sender, e) => { UncheckAction(exp); };
        }

        public void CheckAction(Exp exp)
        {
            CheckedExp.Add(exp);
        }

        public void UncheckAction(Exp exp)
        {
            CheckedExp.Remove(exp);
        }
    }
}
