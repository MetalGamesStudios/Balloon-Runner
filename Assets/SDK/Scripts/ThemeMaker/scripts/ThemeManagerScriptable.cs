using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MetalGamesSDK;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(fileName = "ThemeManager", menuName = "ScriptableObjects/ThemeManager")]
public class ThemeManagerScriptable : SingletonScriptableObject<ThemeManagerScriptable>
{
    public MaterialContainer materialContainer;
    [Space(10)] public List<Theme> themes = new List<Theme>();

    public Theme CurrentTheme;

    // [Button]
    // public void BindEvents()
    // {
    //     foreach (var theme in themes)
    //     {
    //         foreach (var color in theme.Colors)
    //         {
    //             color.onValueChange += delegate(Color i_color) { ColorBinder(color.id, i_color); };
    //         }
    //
    //         SkyBoxEventBinders(theme);
    //     }
    // }


    


   
    void ChangeMaterialColor(Material i_material, Color i_color)
    {
        i_material.color = i_color;
    }


    void ChangeHorizonSkyboxColor(Material i_material, string i_propertyname, Color i_color)
    {
        i_material.SetColor(i_propertyname, i_color);
    }

    void ChangeHorizonSkyboxStrenght(Material i_material, string i_propertyname, float i_strenght)
    {
        i_material.SetFloat(i_propertyname, i_strenght);
    }
}