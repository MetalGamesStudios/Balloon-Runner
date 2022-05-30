using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MetalGamesSDK;

public class StorageManagerBase : Singleton<StorageManager>
{
#if UNITY_EDITOR
    [OnInspectorGUI, PropertyOrder(0)]
    private void ShowImage()
    {
        GUILayout.Label(
            UnityEditor.AssetDatabase.LoadAssetAtPath<Texture>("Assets/MuhammadTouseefSDK/Textures/StoreManager.png"));
    }

#endif

    [TitleGroup("Persistence Values", "General"), PropertyOrder(1)]
    [ShowInInspector]
    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt(nameof(CurrentLevel)); }
        set
        {
            PlayerPrefs.SetInt(nameof(CurrentLevel), value);
            HUDManager.Instance.UpdateLevelTxt();
        }
    }

    [ShowInInspector, PropertyOrder(3)] public SettingsVars SettingsPanelVariables;

    [ShowInInspector, PropertyOrder(3), PropertySpace(8)]
    public CurrencyStruct Wallet;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstGameStart"))
        {
            PlayerPrefs.SetInt("FirstGameStart", 1);
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("SFX", 1);
        }
    }

    public struct SettingsVars
    {
        [ShowInInspector]
        public bool Music
        {
            set { PlayerPrefs.SetInt(nameof(Music), value.ToInt()); }
            get
            {
                if (PlayerPrefs.GetInt(nameof(Music)) == 0) return false;
                return true;
            }
        }


        [ShowInInspector]
        public bool SFX
        {
            set { PlayerPrefs.SetInt(nameof(SFX), value.ToInt()); }
            get
            {
                if (PlayerPrefs.GetInt(nameof(SFX)) == 0) return false;
                else return true;
            }
        }

        [ShowInInspector]
        public bool Haptics
        {
            set { PlayerPrefs.SetInt(nameof(Haptics), value.ToInt()); }
            get
            {
                if (PlayerPrefs.GetInt(nameof(Haptics)) == 0) return false;
                else return true;
            }
        }
    }


    public struct CurrencyStruct
    {
        [ShowInInspector]
        public int Currency
        {
            set
            {
                PlayerPrefs.SetInt(nameof(Currency), value);
                GameManager.Instance.CurrencyChanged(value);
#if UNITY_EDITOR
                HUDManager.Instance.UpdateCurrencyTxt();
#endif
            }


            get { return PlayerPrefs.GetInt(nameof(Currency)); }
        }

        [ShowInInspector, Button]
        public void Increment()
        {
            Currency++;
        }
    }
}