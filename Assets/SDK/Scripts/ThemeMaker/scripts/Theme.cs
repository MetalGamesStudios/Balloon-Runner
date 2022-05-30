// using System;

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// using TMPro;
// using UnityEditor.ShaderGraph.Internal;
// using UnityEngine.UI;

[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/Theme")]
public class Theme : ScriptableObject
{
    protected MaterialContainer _materialContainer;
    public List<customTexturecolor> Texturecolors;
    public List<customcolor> Colors;
    public skyboxcolors skyboxcolors;
    public FogProps FogProperties;


    [Button]
    public void ApplyThisTheme()
    {
        ApplyColors();
    }

    public void  ApplyThisTheme(Theme theme)
    {
        ApplyColors();
        theme = this;
    }

    void ApplyColors()
    {
        foreach (var _customcolor in Colors)
        {
            _customcolor.action();
        }

        foreach (var custom_texturecolor in Texturecolors)
        {
            custom_texturecolor.action();
        }

        skyboxcolors.ApplyskyboxColors();
        FogProperties.ApplyFog();
    }
}

[System.Serializable]
public class customcolor
{
    private MaterialContainer _materialContainer => MaterialContainer.Instance;
    public string id;
    [OnValueChanged("action")] public Color _color;

    public void action()
    {
        var materials = _materialContainer.Materials;
        foreach (var mat in materials)
        {
            if (mat.id == id)
                mat.material.color = _color;
        }
    }
}

[System.Serializable]
public class customTexturecolor
{
    public string id;
    [OnValueChanged("action")] public Color _color;
    [OnValueChanged("action")] public Texture texture;

    public void action()
    {
        var materials = MaterialContainer.Instance.Materials;

        foreach (var mat in materials)
        {
            if (mat.id == id)
            {
                mat.material.color = _color;
                mat.material.mainTexture = texture;
            }
        }
    }
}


[System.Serializable]
public class skyboxcolors
{
    [OnValueChanged(nameof(topcolor))] public Color _topColor;
    [OnValueChanged(nameof(horizoncolor))] public Color _horizonColor;
    [OnValueChanged(nameof(bottomcolor))] public Color _bottomColor;
    [OnValueChanged(nameof(topstrenght))] public float _topColorStrenght;

    [OnValueChanged(nameof(bottomstrenght))]
    public float _bottomColorStrenght;

    void topcolor()
    {
        Material SkyboxMat = MaterialContainer.Instance.skyBxMaterial;
        ChangeHorizonSkyboxColor(SkyboxMat, PropertyNames.topColorPropertyName, _topColor);
    }

    void bottomcolor()
    {
        Material SkyboxMat = MaterialContainer.Instance.skyBxMaterial;
        ChangeHorizonSkyboxColor(SkyboxMat, PropertyNames.bottomColorPropertyName, _bottomColor);
    }

    void horizoncolor()
    {
        Material SkyboxMat = MaterialContainer.Instance.skyBxMaterial;
        ChangeHorizonSkyboxColor(SkyboxMat, PropertyNames.horizonColorPropertyName, _horizonColor);
    }

    void topstrenght()
    {
        Material SkyboxMat = MaterialContainer.Instance.skyBxMaterial;
        ChangeHorizonSkyboxStrenght(SkyboxMat, PropertyNames.topStrenghtPropertyName, _topColorStrenght);
    }

    void bottomstrenght()
    {
        Material SkyboxMat = MaterialContainer.Instance.skyBxMaterial;
        ChangeHorizonSkyboxStrenght(SkyboxMat, PropertyNames.bottomStrenghtPropertyName, _bottomColorStrenght);
    }

    public void ApplyskyboxColors()
    {
        topcolor();
        topstrenght();

        bottomcolor();
        bottomstrenght();

        horizoncolor();
    }

    void ChangeHorizonSkyboxColor(Material i_material, string i_propertyname, Color i_color)
    {
        i_material.SetColor(i_propertyname, i_color);
    }

    void ChangeHorizonSkyboxStrenght(Material i_material, string i_propertyname, float i_strenght)
    {
        i_material.SetFloat(i_propertyname, i_strenght);
    }

    public struct PropertyNames
    {
        [Header("Not For Artist")] [Header("Properties")]
        public static string bottomColorPropertyName = "_SkyColor3";

        public static string topColorPropertyName = "_SkyColor1";
        public static string horizonColorPropertyName = "_SkyColor2";
        public static string topStrenghtPropertyName = "_SkyExponent1";
        public static string bottomStrenghtPropertyName = "_SkyExponent2";
    }
}

[System.Serializable]
public class FogProps
{
    [OnValueChanged(nameof(startvalue))] [Range(-10, 100)]
    public float _fogStartValue = 100;

    [OnValueChanged(nameof(endvale))] [Range(0, 200)]
    public float _fogEndValue = 200;

    [OnValueChanged(nameof(fogcolor))] public Color _fogColor = Color.white;
    public void startvalue() => RenderSettings.fogStartDistance = _fogStartValue;
    public void endvale() => RenderSettings.fogEndDistance = _fogEndValue;
    public void fogcolor() => RenderSettings.fogColor = _fogColor;

    public void ApplyFog()
    {
        startvalue();
        endvale();
        fogcolor();
    }
}