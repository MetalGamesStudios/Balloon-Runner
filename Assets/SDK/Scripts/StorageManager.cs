using System;
using MetalGamesSDK;
using MetalGamesSDK.SavingSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class
    StorageManager : StorageManagerBase
{
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("welcome"))
        {
            SaveData();
            PlayerPrefs.SetInt("welcome", 1);
        }

        LoadData();
    }

    #region DataFunctions

    [Button, PropertyOrder(100), PropertySpace(20)]
    public void ResetData()
    {
        Data data = new Data(1, 0);
        SaveSystem.SaveData(data);
        LoadData();
    }

    [Button, PropertyOrder(101)]
    public void LoadData()
    {
        Data data = SaveSystem.LoadData();
        CurrentLevel = data.CurrentLevel;
        Wallet.Currency = data.Currency;
    }

    [Button, PropertyOrder(102)]
    public void SaveData()
    {
        Data data = new Data(CurrentLevel, Wallet.Currency);
        SaveSystem.SaveData(data);
    }

    private void OnDestroy()
    {
        SaveData();
    }

    #endregion
}