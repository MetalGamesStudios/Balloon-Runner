using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetalGamesSDK;
using System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolManagerBase : Singleton<PoolManager>
{
#if UNITY_EDITOR
    [OnInspectorGUI, PropertyOrder(-10)]
    private void ShowImage()
    {
        GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/MuhammadTouseefSDK/Textures/PoolManager.png"),
            EditorStyles.centeredGreyMiniLabel);
    }

#endif

    #region Definations

    [PropertyOrder(1)] public List<Item> Items = new List<Item>();


    [Title("Holders"), Space(30)] [ReadOnly, ShowInInspector, PropertyOrder(2)]
    public List<HolderData> HoldersInfo = new List<HolderData>();


    [Button("Prebake"), PropertyOrder(100), PropertySpace(50)]
    public void PreBake()
    {
        RemoveAllHolders();
        CreateTypeHolders();
    }

    [Button("Prebake Particles Systems"), PropertyOrder(100), PropertySpace(50)]
    public void Bakepartcles()
    {
        CreateParticlesHolders();
    }

    [Button, PropertyOrder(101)]
    public void Clear()
    {
        RemoveAllHolders();
    }

    #endregion

    #region Methods

    void CreateTypeHolders()
    {
        foreach (Item i_item in Items)
        {
            if (!(i_item.Key == ePoolType.Fire || i_item.Key == ePoolType.Fire1 || i_item.Key == ePoolType.Fire2))
            {
                GameObject newHolder = new GameObject(i_item.Key.ToString() + " Holder");

                Holders holderComponent = newHolder.AddComponent<Holders>();
                holderComponent.Holder = newHolder;
                holderComponent.Key = i_item.Key;

                newHolder.transform.SetParent(transform);


                GeneratePrefabs(i_item, holderComponent);

                HolderData data = new HolderData();
                data.HolderScript = holderComponent;
                data.Holder = newHolder;
                HoldersInfo.Add(data);
            }
        }
    }

    void CreateParticlesHolders()
    {
        foreach (Item i_item in Items)
        {
            if (i_item.Key == ePoolType.Fire || i_item.Key == ePoolType.Fire1 || i_item.Key == ePoolType.Fire2)
            {
                GameObject newHolder = new GameObject(i_item.Key.ToString() + " Holder");

                Holders holderComponent = newHolder.AddComponent<Holders>();
                holderComponent.Holder = newHolder;
                holderComponent.Key = i_item.Key;

                newHolder.transform.SetParent(transform);
                GenerateParticlesPrefabs(i_item, holderComponent);

                HolderData data = new HolderData();
                data.HolderScript = holderComponent;
                data.Holder = newHolder;
                HoldersInfo.Add(data);
            }
        }
    }


    void RemoveAllHolders()
    {
        foreach (var i_gm in HoldersInfo)
        {
            DestroyImmediate(i_gm.Holder);
        }

        HoldersInfo.Clear();
    }

    void GeneratePrefabs(Item i_Item, Holders i_Holder)
    {
        for (int x = 0; x < i_Item.InitialCount; x++)
        {
            GameObject Obj = Instantiate(i_Item.Prefab);


            i_Holder.AvailableItems.Push(Obj);
            Obj.SetActive(false);

            Obj.transform.position = Vector3.zero;
            Obj.name = "P_" + (x + 1).ToString();
            Obj.transform.SetParent(i_Holder.Holder.transform);
        }
    }

    void GenerateParticlesPrefabs(Item i_Item, Holders i_Holder)
    {
        for (int x = 0; x < i_Item.InitialCount; x++)
        {
            var Obj = Instantiate(i_Item.Prefab).GetComponent<TrailRenderer>();


            i_Holder.availableItemsParticleSystems.Push(Obj);
            Obj.gameObject.SetActive(false);

            Obj.transform.position = Vector3.zero;
            Obj.name = "P_" + (x + 1).ToString();
            Obj.transform.SetParent(i_Holder.Holder.transform);
        }
    }

    #endregion
}


[Serializable]
public class HolderData
{
    [HideInInspector] public Holders HolderScript;
    public GameObject Holder;
}

[Serializable]
public class Item
{
    [HorizontalGroup("Base"), LabelWidth(30), HideLabel]
    public ePoolType Key;

    [BoxGroup("Base/Properties"), LabelWidth(100)]
    public int InitialCount;

    [BoxGroup("Base/Properties"), LabelWidth(50)]
    public GameObject Prefab;
}