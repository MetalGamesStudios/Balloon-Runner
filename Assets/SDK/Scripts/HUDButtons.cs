using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.UI;
using MetalGamesSDK;
public class HUDButtons : Singleton<HUDButtons>
{

    public Buttons Buttons;

    private void OnEnable()
    {
        Buttons.StartButton.onClick.AddListener(StartLevel);
        Buttons.SettingsButton.onClick.AddListener(ShowSettings);
        Buttons.SettingsCloseButton.onClick.AddListener(CloseSettings);
        Buttons.SfxButton.onClick.AddListener(SettingsManager.Instance.ToggleSfx);
        Buttons.MusicButton.onClick.AddListener(SettingsManager.Instance.ToggleMusic);
        Buttons.VibrationButton.onClick.AddListener(SettingsManager.Instance.ToggleVibrations);
        Buttons.NextLevelButton.onClick.AddListener(GameManager.Instance.NextLevel);
        Buttons.RestartButton.onClick.AddListener(GameManager.Instance.RestartLevel);
    }

    void StartLevel()
    {
        GameManager.Instance.StartLevel();
    }
    void ShowSettings()
    {
        HUDManager.Instance.ShowSettings();
    }
    void CloseSettings()
    {
        HUDManager.Instance.HideSettings();
    }




    [Button]
    public void GetButtons()
    {
        Buttons.StartButton = HUDManager.Instance.transform.FindDeepChild<Button>("startbutton");
        Buttons.SettingsButton = HUDManager.Instance.transform.FindDeepChild<Button>("settingbutton");
        Buttons.SettingsCloseButton = HUDManager.Instance.transform.FindDeepChild<Button>("settingclosebutton");
        Buttons.SfxButton = HUDManager.Instance.transform.FindDeepChild<Button>("sfxbutton");
        Buttons.MusicButton = HUDManager.Instance.transform.FindDeepChild<Button>("musicbutton");
        Buttons.VibrationButton = HUDManager.Instance.transform.FindDeepChild<Button>("vibrationbutton");
        Buttons.NextLevelButton = HUDManager.Instance.transform.FindDeepChild<Button>("nextlevelbutton");
        Buttons.RestartButton = HUDManager.Instance.transform.FindDeepChild<Button>("restartbutton");

    }

}

[Serializable]

public class Buttons
{
    [ReadOnly] public Button StartButton;
    [ReadOnly] public Button SettingsButton;
    [ReadOnly] public Button SettingsCloseButton;
    [ReadOnly] public Button SfxButton;
    [ReadOnly] public Button MusicButton;
    [ReadOnly] public Button VibrationButton;
    [ReadOnly] public Button NextLevelButton;
    [ReadOnly] public Button RestartButton;
}