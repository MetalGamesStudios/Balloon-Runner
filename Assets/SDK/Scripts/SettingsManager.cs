using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MetalGamesSDK;
using DG.Tweening;

public class SettingsManager : Singleton<SettingsManager>
{
    [ReadOnly, SerializeField, PropertyOrder(0)]
    RectTransform m_SfxImg;

    [ReadOnly, SerializeField, PropertyOrder(0)]
    RectTransform m_MusicImg;

    [ReadOnly, SerializeField, PropertyOrder(0)]
    RectTransform m_VibrationImg;

    [Button, PropertySpace(1, 1), PropertyOrder(1)]
    public void GetReferences()
    {
        m_SfxImg = HUDButtons.Instance.Buttons.SfxButton.transform.GetChild(0).GetComponent<RectTransform>();
        m_MusicImg = HUDButtons.Instance.Buttons.MusicButton.transform.GetChild(0).GetComponent<RectTransform>();
        m_VibrationImg = HUDButtons.Instance.Buttons.VibrationButton.transform.GetChild(0)
            .GetComponent<RectTransform>();
    }

    [PropertySpace(1, 1), PropertyOrder(2)]
    private void Start()
    {
        if (!StorageManager.Instance.SettingsPanelVariables.SFX)
            m_SfxImg.DOLocalMoveX(m_SfxImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);

        if (!StorageManager.Instance.SettingsPanelVariables.Haptics)
            m_VibrationImg.DOLocalMoveX(m_VibrationImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
        if (!StorageManager.Instance.SettingsPanelVariables.Music)
            m_MusicImg.DOLocalMoveX(m_MusicImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
    }

    public void ToggleSfx()
    {
        if (StorageManager.Instance.SettingsPanelVariables.SFX)
        {
            Debug.Log("SfxMuted");
            m_SfxImg.DOLocalMoveX(m_SfxImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.SFX = false;
        }
        else
        {
            m_SfxImg.DOLocalMoveX(m_SfxImg.localPosition.x + GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.SFX = true;
            Debug.Log("SfxUnmuted");
        }
    }

    public void ToggleMusic()
    {
        if (StorageManager.Instance.SettingsPanelVariables.Music)
        {
            Debug.Log("MusicMuted");
            m_MusicImg.DOLocalMoveX(m_MusicImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.Music = false;
        }
        else
        {
            m_MusicImg.DOLocalMoveX(m_MusicImg.localPosition.x + GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.Music = true;
            Debug.Log("Music Unmuted");
        }

        AudioManager.instance.CheckMuteState();
    }

    public void ToggleVibrations()
    {
        if (StorageManager.Instance.SettingsPanelVariables.Haptics)
        {
            Debug.Log("No Vibrations");
            m_VibrationImg.DOLocalMoveX(m_VibrationImg.localPosition.x - GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.Haptics = false;
        }
        else
        {
            m_VibrationImg.DOLocalMoveX(m_VibrationImg.localPosition.x + GameConfig.Instance.HUD.MoveFactor,
                GameConfig.Instance.HUD.ToggleTime).SetUpdate(true);
            StorageManager.Instance.SettingsPanelVariables.Haptics = true;
            Debug.Log("Vibrations Enabled");
        }
    }
}