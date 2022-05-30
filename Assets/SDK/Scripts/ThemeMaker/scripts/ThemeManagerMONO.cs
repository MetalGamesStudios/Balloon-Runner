using System;
using UnityEngine;
using MetalGamesSDK;
using Random = UnityEngine.Random;

public class ThemeManagerMONO : Singleton<ThemeManagerMONO>
{
    public Theme[] themes;
    public Theme currentTheme;

    private void OnEnable()
    {
        GameManager.OnLeveLoadComplete += ApplyRandomTHeme;
    }

    private void OnDisable()
    {
        GameManager.OnLeveLoadComplete -= ApplyRandomTHeme;
    }

    void ApplyRandomTHeme()
    {
        if (themes.Length == 0)
            return;
        var ran = Random.Range(0, themes.Length);
        themes[ran].ApplyThisTheme(currentTheme);
    }
}