using System;
using System.Collections;
using System.Collections.Generic;
using C_;
using Sirenix.OdinInspector;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public List<Balloon> balloons = new List<Balloon>();
    [ShowInInspector] public GameObject obstacle;
 private Collider[] _colliders;

 private void Awake()
 {
     _colliders = GetComponents<Collider>();
 }

 public void RemoveBalloon()
    {
        var balloon = balloons[0];
        if (balloons.Contains(balloon))
        {
            balloon.DisableBallon();
            balloons.Remove(balloon);
        }

        if (balloons.Count == 0)
        {
            foreach (var VARIABLE in _colliders)
            {
                VARIABLE.enabled = false;
            }
            obstacle.GetComponent<IObstacle>().Drop();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out fire fire))
        {
            RemoveBalloon();
        }
    }
}