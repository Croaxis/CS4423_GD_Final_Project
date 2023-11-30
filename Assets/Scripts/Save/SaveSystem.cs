using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace cs4423fp.Save
{
    public static class SaveSystem
    {
        public static string[] GetSaveFiles()
        {
            // Retrieve a list of save files in the persistent data path
            return Directory.GetFiles(Application.persistentDataPath, "*.sav");
        }

        public static void SaveGame(string saveFileName)
        {
            // BinaryFormatter formatter = new BinaryFormatter();
            // string path = Path.Combine(Application.persistentDataPath, saveFileName + ".sav");
            // FileStream stream = new FileStream(path, FileMode.Create);

            // SaveData data = new SaveData();

            // // Add logic to fill data based on your game state
            // // data.playerUnits = ...

            // formatter.Serialize(stream, data);
            // stream.Close();
        }

        public static void LoadGame( string saveFileName)
        {
            // string path = Path.Combine(Application.persistentDataPath, saveFileName + ".sav");
            // if (File.Exists(path))
            // {
            //     BinaryFormatter formatter = new BinaryFormatter();
            //     FileStream stream = new FileStream(path, FileMode.Open);

            //     SaveData data = formatter.Deserialize(stream) as SaveData;
            //     stream.Close();

            //     // Add logic to load data into the game based on data
            //     // playerController.PlayerUnits = data.playerUnits;
            // }
            // else
            // {
            //     Debug.LogError("Save file not found in " + path);
            // }
        }
    }
}
