using Core.Audio;

[System.Serializable]
public class ConfigurationData
{
    public int masterVolume;
    public int musicVolume;
    public int soundVolume;

    public ConfigurationData(AudioManager audio)
    {
        masterVolume = audio.masterVolume;
        musicVolume = audio.musicVolume;
        soundVolume = audio.soundVolume;
    }
}
