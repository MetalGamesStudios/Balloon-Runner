using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;

public class HammerCollider : MonoBehaviour
{
    private List<Transform> _collidedAlready = new List<Transform>();

    void Interact(PlayerController2 playerController2)
    {
        var charatcertr = playerController2.animancerController.animancerComponent.Animator.transform;

        var animancer = playerController2.animancerController;
        animancer.animancerComponent.Animator.applyRootMotion = true;
        animancer.Limping();
        playerController2.rigBuilder.enabled = false;

        var prevspeed = playerController2.movementProps.playerForwardSpeed;
        playerController2._forwardSpeed = 2;
        playerController2.firingController.enabled = false;
        playerController2.canMove = false;


        var animationData = playerController2.animancerController.data;
        float time = animationData.limping.Transition.Clip.length;
        time++;

        AudioManager.instance.Play("grunt");

        CameraSetup.Instance.ShakeCameraInvoker();
        HapticsManager.Instance.StrongHaptic();
        DOVirtual.DelayedCall(time, delegate
        {
            animancer.animancerComponent.Animator.applyRootMotion = false;
            animancer.RunAnimation();
            charatcertr.transform.Reset();

            playerController2._forwardSpeed = prevspeed;
            playerController2.firingController.enabled = true;
            playerController2.canMove = true;
        });
    }

    void Interact(EnemyMovement enemyController)
    {
        var charatcertr = enemyController.animancerController.animancerComponent.Animator.transform;

        var animancer = enemyController.animancerController;
        animancer.animancerComponent.Animator.applyRootMotion = true;


        animancer.Limping();
        enemyController.rigBuilder.enabled = false;

        var prevspeed = enemyController.mForwardSpeed;
        enemyController.mForwardSpeed = 2;
        enemyController.firingController.enabled = false;
        enemyController.canMove = false;


        var animationData = enemyController.animancerController.data;


        float time = animationData.limping.Transition.Clip.length;
        time++;


        DOVirtual.DelayedCall(time, delegate
        {
            animancer.animancerComponent.Animator.applyRootMotion = false;
            animancer.RunAnimation();
            charatcertr.transform.Reset();
            enemyController.mForwardSpeed = prevspeed;
            enemyController.firingController.enabled = true;
            enemyController.canMove = true;
        });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController2 playerController))
        {
            if (!_collidedAlready.Contains(other.transform))
                Interact(playerController);

            _collidedAlready.Add(other.transform);
        }
        else if (other.TryGetComponent(out EnemyMovement mv))
        {
            if (!_collidedAlready.Contains(other.transform))
                Interact(mv);
            _collidedAlready.Add(other.transform);
        }
    }
}