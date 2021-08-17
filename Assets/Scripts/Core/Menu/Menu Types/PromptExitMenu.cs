using UnityEngine;
using Core.Menu.MenuSystem;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class PromptExitMenu : SimpleMenu<PromptExitMenu>
            {
                public void OnYes()
                {
                    Application.Quit();
                }
            }
        }
    }
}

