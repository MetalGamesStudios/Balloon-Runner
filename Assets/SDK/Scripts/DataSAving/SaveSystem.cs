using System.Collections;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace MetalGamesSDK.SavingSystem
{
    public static class SaveSystem
    {
        public static string SavePath = Application.persistentDataPath + "/Save";

        public static void SaveData(Data i_Data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(SavePath, FileMode.Create);
            Data data = new Data(i_Data.CurrentLevel, i_Data.Currency);

            binaryFormatter.Serialize(stream, data);

            stream.Close();
        }

        public static Data LoadData()
        {
            Data data = null;
            if (File.Exists(SavePath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(SavePath, FileMode.Open);
                data = binaryFormatter.Deserialize(stream) as Data;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("There is No Data to at " + SavePath);
                return null;
            }
        }
    }
}