using System.Collections;
using System.Collections.Generic;
using C_;
using UnityEngine;
using UnityEngine.UIElements;

public class Saw : MonoBehaviour, IObstacle
{
    public Rigidbody BOx;

    public void Drop()
    {
        BOx.isKinematic = false;
        BOx.useGravity = true;
        BOx.velocity = new Vector3(0, -20, 0);
    }
}