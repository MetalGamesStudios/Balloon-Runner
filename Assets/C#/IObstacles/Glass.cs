using System.Collections;
using System.Collections.Generic;
using C_;
using DG.Tweening;
using MetalGamesSDK;
using UnityEngine;

public class Glass : MonoBehaviour, IObstacle
{
    public Transform glassTr;


    public void Drop()
    {
        var config = GameConfig.Instance.obstacleProps;
        glassTr.DOLocalRotate(Vector3.forward * config.rotationAmount, config.timeToRotate);
    }
}