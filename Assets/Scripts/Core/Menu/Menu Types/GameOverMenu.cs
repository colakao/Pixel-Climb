using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class GameOverMenu : SimpleMenu<GameOverMenu>
            {
                public override void OnBackPressed()
                {
                    Close();
                    GameManager.Instance.ReloadNormal();
                    GameMenu.Show();
                }

                public void OnMainMenu()
                {
                    Close();
                    MainMenu.Show();
                }

                public void OnWatchAd()
                {
                    PromptAdMenu.Show();
                }
            }
        }
    }
}
