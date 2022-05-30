using System;
using System.Collections;
using System.Collections.Generic;
using MetalGamesSDK;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorChooserNPC : MonoBehaviour
{
    public Renderer crown;
    public Renderer npcRendere;


    void Start()
    {
        var config = GameConfig.Instance.npcProperties;

        var mat = config.materials[Random.Range(0, config.materials.Length)];

        crown.material = mat;
        npcRendere.material = mat;
        
    }
}