using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class TutorialMenu : SimpleMenu<TutorialMenu>
            {
                public void OnOk()
                {
                    Hide();
                    MainMenu.Close();
                    GameManager.Instance.Unpaused();
                    GameMenu.Show();
                    GameManager.Instance.SavePlayer();
                }

                public override void OnBackPressed()
                {
                    Hide();
                    MainMenu.Open();
                }
            }
        }
    }
}

