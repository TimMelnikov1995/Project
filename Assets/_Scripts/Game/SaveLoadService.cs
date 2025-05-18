using System;
using UnityEngine;
using Newtonsoft.Json; // com.unity.nuget.newtonsoft-json

public class SaveLoadService
{
    public SaveData CurrentSaveData;

    public event Action EOn_Load;
    public event Action EOn_Save;

    void OnLoad()
    {
        EOn_Load?.Invoke();
    }



    public void Save()
    {
        EOn_Save?.Invoke();
        string saveDataToJSON = JsonConvert.SerializeObject(CurrentSaveData);
        PlayerPrefs.SetString("Data", saveDataToJSON);
    }

    public void Load()
    {
        string saveDataInPrefs = PlayerPrefs.GetString("Data");
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(saveDataInPrefs);

        if (saveData != null)
            CurrentSaveData = saveData;
        else
            CurrentSaveData = new SaveData();

        // If there is a database:
        // Enter code that will request saving from the database and, upon response from it, call OnLoad.

        OnLoad();
    }

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }



    public class SaveData
    {
        public int LastLevelIndex;
    }
}