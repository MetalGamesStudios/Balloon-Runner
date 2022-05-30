using System.Collections.Generic;
using MetalGamesSDK;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "MaterialContainer", menuName = "ScriptableObjects/MaterialContainer")]
public class MaterialContainer : SingletonScriptableObject<MaterialContainer>
{
    public CustomMaterial[] Materials;
    public Material skyBxMaterial;
    public List<PropertyBlock> SkyBoxPropertyBlocks = new List<PropertyBlock>();

    public void GetSkyboxProperties()
    {
        GetMaterialProperties(skyBxMaterial, SkyBoxPropertyBlocks);
    }

    public CustomMaterial GetMaterialById(string id)
    {
        foreach (var VARIABLE in Materials)
        {
            if (VARIABLE.id == id)
                return VARIABLE;
        }

        return null;
    }

    public static void GetMaterialProperties(Material i_material, List<PropertyBlock> propertiesList)
    {
        int count = i_material.shader.GetPropertyCount();

        for (int i = 0; i < count; i++)
        {
            Shader shader = i_material.shader;
            PropertyBlock property_block = new PropertyBlock();

            property_block.propertyName = shader.GetPropertyName(i);
            property_block.propertyType = shader.GetPropertyType(i);

            propertiesList.Add(property_block);
        }
    }
}

[System.Serializable]
public class CustomMaterial
{
    public string id;
    public Material material;
    public List<PropertyBlock> Properties = new List<PropertyBlock>();
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

[System.Serializable]
public struct PropertyBlock
{
    public string propertyName;
    public ShaderPropertyType propertyType;
}