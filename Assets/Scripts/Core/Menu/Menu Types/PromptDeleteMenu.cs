using Core.Menu.MenuSystem;
using Core.Save;
using Core.Audio;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class PromptDeleteMenu : SimpleMenu<PromptDeleteMenu>
            {
                public void OnYes()
                {
                    SaveSystem.Delete();
                    GameManager.Instance.LoadPlayer();
                    AudioManager.Instance.LoadConfiguration();
                    OptionsMenu.Instance.UpdateSliderValues();
                }
            }
        }
    }
}

