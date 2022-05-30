using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using MetalGamesSDK;
using Random = UnityEngine.Random;

public class HUDManager : HUDManagerBase
{
    List<GameObject> m_ClosedActiveScreens = new List<GameObject>();

    

    private void Start()
    {
    
    }

    public void ShowSettings()
    {
        Time.timeScale = 0;

        References.BgPanel.gameObject.SetActive(true);

        m_ClosedActiveScreens.Clear();
        m_ClosedActiveScreens = new List<GameObject>(ActiveScreens);

        TurnOffAllScreens();
        ShowScreen(References.SettingsPanel);
    }

    public void HideSettings()
    {
        TurnOnScreens();

        HideScreen(References.SettingsPanel, delegate()
        {
            References.SettingsPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
    }

    void TurnOnScreens()
    {
        foreach (GameObject gm in m_ClosedActiveScreens)
        {
            ActivateScreenObj(gm);
        }
    }

   
}