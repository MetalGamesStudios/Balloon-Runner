using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Animancer;
using MetalGamesSDK;
using UnityEngine;


public class AnimancerController : MonoBehaviour
{
    public AnimancerComponent animancerComponent;
    public AniamationData data;
    public bool IsPlayer;
    public AvatarMask lowerMASK;
    public AvatarMask UpperMask;

    public enum ANimationSttae
    {
        none,
        idle,
        Run,
        RunFull,
        Limping,
        victory
    }

    private void OnEnable()
    {
        GameManager.OnLevelStarted += RunAnimation;
        GameManager.OnLeveLoadComplete += IdleAnimation;
    }


    private void OnDisable()
    {
        GameManager.OnLevelStarted -= RunAnimation;
        GameManager.OnLeveLoadComplete -= IdleAnimation;
    }

    public ANimationSttae animationState;

    public void RunWithGun()
    {
        if (animationState != ANimationSttae.Run)
        {
            animancerComponent.Layers[0].SetMask(lowerMASK);
            animancerComponent.Layers[0].Play(data.Run);
            animationState = ANimationSttae.Run;
        }
    }

    public void RunAnimation()
    {
        if (animationState != ANimationSttae.RunFull)
        {
            animancerComponent.Layers[0].SetMask(null);
            animancerComponent.Play(data.Run);
            animationState = ANimationSttae.RunFull;
        }
    }

    public void IdleAnimation()
    {
        if (animationState == ANimationSttae.idle)
            return;

        animancerComponent.Layers[0].SetMask(null);
        animancerComponent.Play(data.Idle);

        animationState = ANimationSttae.idle;
    }

    public void GrabGun()
    {
        // animancerComponent.Layers[1].SetMask(UpperMask);
        // animancerComponent.Layers[1].Play(data.grabGun);
    }

    public void PutBackGun()
    {
        // animancerComponent.Layers[1].SetMask(UpperMask);
        // animancerComponent.Layers[1].Play(data.putBackGun);
    }

    public void Limping()
    {
        if (animationState != ANimationSttae.Limping)
        {
            ClipTransition clip = null;
            int random = UnityEngine.Random.Range(0, 5);

            switch (random)
            {
                case 1:
                    clip = data.hurt1;
                    break;
                case 2:
                    clip = data.hurt2;
                    break;
                case 3:

                    clip = data.hurt3;
                    break;
                case 4:
                    clip = data.hurt4;
                    break;
                default:
                    clip = data.hurt2;
                    break;
            }

            animancerComponent.Play(clip);


            animationState = ANimationSttae.Limping;
        }
    }

    public void DefeatAnimation()
    {
        animancerComponent.Play(data.defeat);
    }

    public void WinAnimation()
    {
        animancerComponent.Play(data.won);
    }
}