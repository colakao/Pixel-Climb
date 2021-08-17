using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Audio;

namespace Core
{
    namespace Save
    {
        public static class SaveSystem
        {
            private static string PlayerDataPath
            {
                get
                {
                    return Application.persistentDataPath + "/playerData.bin";
                }
            }

            private static string ConfigurationDataPath
            {
                get
                {
                    return Application.persistentDataPath + "/configurationData.bin";
                }
            }



            public static void SavePlayer(GameManager gameManager)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(PlayerDataPath, FileMode.Create);

                PlayerData data = new PlayerData(gameManager);

                formatter.Serialize(stream, data);
                stream.Close();
            }

            public static PlayerData LoadPlayer()
            {
                if (File.Exists(PlayerDataPath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(PlayerDataPath, FileMode.OpenOrCreate);

                    PlayerData data = (PlayerData)formatter.Deserialize(stream);
                    stream.Close();

                    return data;
                }
                else
                {
                    Debug.LogError("Save file not found in " + PlayerDataPath);
                    return null;
                }
            }
            public static void SaveConfiguration(AudioManager audio)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(ConfigurationDataPath, FileMode.Create);

                ConfigurationData data = new ConfigurationData(audio);

                formatter.Serialize(stream, data);
                stream.Close();
            }

            public static ConfigurationData LoadConfiguration()
            {
                if (File.Exists(ConfigurationDataPath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(ConfigurationDataPath, FileMode.OpenOrCreate);

                    ConfigurationData data = (ConfigurationData)formatter.Deserialize(stream);
                    stream.Close();

                    return data;
                }
                else
                {
                    Debug.LogError("Save file not found in " + ConfigurationDataPath);
                    return null;
                }
            }

            public static void Delete()
            {
                try
                {
                    File.Delete(PlayerDataPath);
                    File.Delete(ConfigurationDataPath);
                }
                catch
                {
                    Debug.LogError("Save file not found in '" + PlayerDataPath + "' or doesn't exist.");
                    Debug.LogError("Save file not found in '" + ConfigurationDataPath + "' or doesn't exist.");
                }
            }
        }

    }
}