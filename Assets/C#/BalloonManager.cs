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
    [SerializeField] private Collider _collider;

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
            _collider.enabled = false;
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