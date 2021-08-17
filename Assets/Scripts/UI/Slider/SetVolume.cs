using UnityEngine;
using UnityEngine.Audio;
using Core.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerGroup;

    public bool debug;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerGroup, SliderValueNormalizer.Normalize(sliderValue));
        if (mixerGroup == "MasterVolume")
        {
            AudioManager.Instance.masterVolume = (int)sliderValue;
        }
        if (mixerGroup == "SoundtrackVolume")
        {
            AudioManager.Instance.musicVolume = (int)sliderValue;
        }
        if (mixerGroup == "SFXVolume")
        {
            AudioManager.Instance.soundVolume = (int)sliderValue;
        }
        AudioManager.Instance.SaveConfiguration();
        AudioManager.Instance.StartMusic();
        Log("Volume saved.");
    }

    private void Log(string _msg)
    {
        if (!debug) return;
        Debug.Log("[SetVolume]: "+_msg);
    }
}
