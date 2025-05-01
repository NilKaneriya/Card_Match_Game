using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager 
{
    //private static string SavePath => Application.persistentDataPath + "/savegame.json";
    private static string SavePath => Path.Combine(Application.streamingAssetsPath, "savegame.json");
    
    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }

    

    /// <summary>
    /// here we use for local storage for saving and load 
    /// for secure way use playerprefs and encryption 
    /// </summary>
    
    
    // private const string SaveKey = "EncryptedSaveData";
    // private const string EncryptionKey = "my-secret-key"; // Change this to a stronger key

    // private static string EncryptDecrypt(string data, string key = EncryptionKey)
    // {
    //     char[] keyChars = key.ToCharArray();
    //     char[] dataChars = data.ToCharArray();

    //     for (int i = 0; i < dataChars.Length; i++)
    //     {
    //         dataChars[i] = (char)(dataChars[i] ^ keyChars[i % keyChars.Length]);
    //     }

    //     return new string(dataChars);
    // }

    // public static void SaveGame(SaveData data)
    // {
    //     string json = JsonUtility.ToJson(data);
    //     string encryptedJson = EncryptDecrypt(json);
    //     PlayerPrefs.SetString(SaveKey, encryptedJson);
    //     PlayerPrefs.Save();
    // }

    // public static SaveData LoadGame()
    // {
    //     if (PlayerPrefs.HasKey(SaveKey))
    //     {
    //         string encryptedJson = PlayerPrefs.GetString(SaveKey);
    //         string json = EncryptDecrypt(encryptedJson);
    //         return JsonUtility.FromJson<SaveData>(json);
    //     }

    //     return null;
    // }

    // public static void DeleteSave()
    // {
    //     if (PlayerPrefs.HasKey(SaveKey))
    //     {
    //         PlayerPrefs.DeleteKey(SaveKey);
    //         PlayerPrefs.Save();
    //     }
    // }
}
