using System.Collections;
using System.Collections.Generic;
using C_;
using UnityEngine;

public class SwingMace : MonoBehaviour, IObstacle
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject rope;

    public void Drop()
    {
        _animator.enabled = true;
        rope.SetActive(false);
    }
}