using Core.Menu.MenuSystem;
using Core.Audio;
using UnityEngine.UI;

namespace Core
{
    namespace Menu
    {
        namespace MenuTypes
        {
            public class OptionsMenu : SimpleMenu<OptionsMenu>
            {
                public Slider masterVolume;
                public Slider musicVolume;
                public Slider soundVolume;

                private void OnEnable()
                {
                    UpdateSliderValues();
                }

                public void UpdateSliderValues()
                {
                    masterVolume.value = AudioManager.Instance.masterVolume;
                    musicVolume.value = AudioManager.Instance.musicVolume;
                    soundVolume.value = AudioManager.Instance.soundVolume;
                }
                public void OnDeleteSave()
                {
                    PromptDeleteMenu.Show();
                }
            }
        }
    }
}

