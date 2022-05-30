using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public Material skybox;

    public float timeTomMove;

    public string propertyName;


    void Start()
    {
        skybox.DOFloat(180, propertyName, timeTomMove);
    }
}