using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class PauseMenu : SimpleMenu<PauseMenu>
            {
                public void OnMainMenu()
                {
                    Close();
                    GameMenu.Close();
                    MainMenu.Show();
                    GameManager.Instance.Paused();
                }

                public void OnReplayPressed()
                {
                    GameManager.Instance.ReloadNormal();
                    Close();
                }

                public void OnOptions()
                {
                    OptionsMenu.Show();
                }
                public override void OnBackPressed()
                {
                    Close();
                    GameManager.Instance.Unpaused();
                }
            }

        }
    }
}
