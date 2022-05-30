using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - target.position;
    }


    void Update()
    {
        transform.position = target.position + _offset;
    }
}