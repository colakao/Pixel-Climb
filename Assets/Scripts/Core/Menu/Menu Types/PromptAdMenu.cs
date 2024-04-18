using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class PromptAdMenu : SimpleMenu<PromptAdMenu>
            {
                public void OnYes()
                {
                    Close();
                    GameOverMenu.Close();
                    GameMenu.Show();

                    //AdManager.Instance.PlayRewarded();
                }
            }
        }
    }
}

