using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class GameMenu : SimpleMenu<GameMenu>
            {
                public override void OnBackPressed()
                {
                    PauseMenu.Show();
                    GameManager.Instance.Paused();
                }
            }
        }
    }
}

