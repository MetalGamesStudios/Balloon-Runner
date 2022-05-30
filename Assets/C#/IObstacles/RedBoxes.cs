using System;
using System.Collections;
using System.Collections.Generic;
using C_;
using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class RedBoxes : MonoBehaviour, IObstacle
{
    [SerializeField] Rigidbody boxRb;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Collider[] _collider;

    [Button]
    public void GetReference()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponents<Collider>();
    }

    public void Drop()
    {
        boxRb.isKinematic = false;
    }

    void Interact(PlayerController2 playerController2)
    {
        var animancer = playerController2.animancerController;
        animancer.Limping();

        playerController2.rigBuilder.enabled = false;

        var prevspeed = playerController2.movementProps.playerForwardSpeed;
        playerController2._forwardSpeed = 2;
        playerController2.firingController.enabled = false;
        playerController2.canMove = false;

        _renderer.enabled = false;
        foreach (var VARIABLE in _collider)
        {
            VARIABLE.enabled = false;
        }

        var animationData = playerController2.animancerController.data;
        float time = animationData.limping.Transition.Clip.length;
        time++;

        DOVirtual.DelayedCall(time, delegate
        {
            animancer.RunAnimation();
            playerController2._forwardSpeed = prevspeed;
            playerController2.firingController.enabled = true;
            playerController2.canMove = true;

            gameObject.SetActive(false);
        });
    }

    void Interact(EnemyMovement enemyController)
    {
        var animancer = enemyController.animancerController;
        animancer.Limping();

        enemyController.rigBuilder.enabled = false;

        var prevspeed = enemyController.mForwardSpeed;
        enemyController.mForwardSpeed = 2;
        enemyController.firingController.enabled = false;
        enemyController.canMove = false;

        _renderer.enabled = false;
        foreach (var VARIABLE in _collider)
        {
            VARIABLE.enabled = false;
        }

        var animationData = enemyController.animancerController.data;

        float time = animationData.limping.Transition.Clip.length;
        time++;
        DOVirtual.DelayedCall(time, delegate
        {
            animancer.RunAnimation();
            enemyController.mForwardSpeed = prevspeed;
            enemyController.firingController.enabled = true;
            enemyController.canMove = true;

            gameObject.SetActive(false);
        });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController2 playerController))
        {
            Interact(playerController);
        }
        else if (other.TryGetComponent(out EnemyMovement mv))
            Interact(mv);
    }
}