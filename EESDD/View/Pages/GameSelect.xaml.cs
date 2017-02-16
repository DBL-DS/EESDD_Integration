using EESDD.Class.Control;
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
    /// Interaction logic for GameSelect.xaml
    /// </summary>
    public partial class GameSelect : Page
    {
        public GameSelect()
        {
            InitializeComponent();
        }

        private Game CurrentGame;

        public void ResetPage()
        {
            SetPage(CU.MG_Game.Scenes[0]);
        }

        public void SetPage(Scene scene)
        {
            SetScene(scene);
            SetModes(scene);
            SetSceneButtonVisible(scene);
        }

        private void SetSceneButtonVisible(Scene scene)
        {
            SceneLastButton.Visibility = Visibility.Visible;
            SceneNextButton.Visibility = Visibility.Visible;
            if (CU.MG_Game.Scenes.IndexOf(scene) == 0)
                SceneLastButton.Visibility = Visibility.Hidden;
            if (CU.MG_Game.Scenes.IndexOf(scene) == CU.MG_Game.Scenes.Count - 1)
                SceneNextButton.Visibility = Visibility.Hidden;
        }

        private void SetScene(Scene scene)
        {
            SceneTitleText.Text = scene.Title;
            SceneDetailText.Text = scene.Description;
            SceneImage.Source = FileManager.GetImage(scene.Picture);
        }

        private void NextScene()
        {
            int next = CU.MG_Game.Scenes.IndexOf(CurrentGame.Scene) + 1;
            if (next < CU.MG_Game.Scenes.Count)
                SetPage(CU.MG_Game.Scenes[next]);
        }

        private void LastScene()
        {
            int last = CU.MG_Game.Scenes.IndexOf(CurrentGame.Scene) - 1;
            if (last >= 0)
                SetPage(CU.MG_Game.Scenes[last]);
        }

        private void SetModes(Scene scene)
        {
            List<Game> games = CU.MG_Game.GamesWithScene(scene.Name);
            ChooseGame(games[0]);
            SetModeStack(games);
        }

        private void ChooseGame(Game game)
        {
            CurrentGame = game;
            ModeDetailText.Text = game.Mode.Description;
        }

        private void SetModeStack(List<Game> games)
        {
            ModeStack.Children.Clear();
            foreach (var game in games)
            {
                ModeButton button = new ModeButton();
                button.ModeText = game.Mode.Title;
                button.ModeImage = FileManager.GetImage(game.Mode.Picture);
                button.Click += (sender, e) =>
                {
                    ChooseGame(game);
                    SetChecked(button);
                };
                ModeStack.Children.Add(button);
                if (games.IndexOf(game) == 0)
                    SetChecked(button);
            }
            ModeViewer.ScrollToTop();
        }

        private void SetChecked(ModeButton button)
        {
            foreach (var item in ModeStack.Children)
            {
                (item as ModeButton).IsChecked = false;
            }
            button.IsChecked = true;
        }

        private void SceneLastButton_Click(object sender, RoutedEventArgs e)
        {
            LastScene();
        }

        private void SceneNextButton_Click(object sender, RoutedEventArgs e)
        {
            NextScene();
        }
    }
}
