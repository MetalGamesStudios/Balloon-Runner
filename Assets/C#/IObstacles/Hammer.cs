using System;
using System.Collections;
using System.Collections.Generic;
using C_;
using DG.Tweening;
using MetalGamesSDK;
using UnityEngine;

public class Hammer : MonoBehaviour, IObstacle
{
    [SerializeField] private Transform trToRotate;

    public void Drop()
    {
        trToRotate.DOLocalRotate(new Vector3(0.7f, 90, 180), GameConfig.Instance.obstacleProps.timeToRotate);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"colliding with {other.name}");
    }
}