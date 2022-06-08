using System;
using System.Collections;
using System.Collections.Generic;
using C_;
using DG.Tweening;
using MetalGamesSDK;
using UnityEngine;

public class Gate :MonoBehaviour,IObstacle
{
    public float Yval;
    public float timetoMove;
    [SerializeField]private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private bool isCollided;
    public void Drop()
    {
        _collider.enabled = false;
        transform.DOLocalMoveY(Yval, timetoMove).SetEase(Ease.InBack);
        PlayerController2.Instance._feedBacker.GoodFeedback();
        PlayerController2.Instance._forwardSpeed += 5;
        isCollided = true;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController2 pc2))
        {
           
        }
    }
}
