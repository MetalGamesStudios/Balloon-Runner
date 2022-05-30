using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class fire : MonoBehaviour
{
    public float speed;
    public TrailRenderer renderer;

    private void OnEnable()
    {
        DOVirtual.DelayedCall(0.07f, delegate { renderer.enabled = true; });
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}