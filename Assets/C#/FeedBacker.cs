using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FeedBacker : MonoBehaviour
{
    public Color goodColor, badColor;
    public Renderer renderer;
    public float changeTime;
    private Color prevcol;

    public ParticleSystem happy, sad, evil;

    
    private void Awake()
    {
        DOVirtual.DelayedCall(1, delegate
        {
            prevcol = renderer.material.color;
        });
    }

    public void GoodFeedback()
    {
       
        renderer.material.DOColor(goodColor, changeTime).OnComplete(delegate
        {
            renderer.material.DOColor(prevcol, changeTime);
        });
   if(happy&& !happy.isPlaying)
        happy.Play();
    }

    public void BadFeedback()
    {
       
        renderer.material.DOColor(badColor, changeTime).OnComplete(delegate
        {
            renderer.material.DOColor(prevcol, changeTime);
        });
        
       if(sad &&!sad.isPlaying)
        sad.Play();
    }

    public void EvilFeedback()
    {
        evil.Play();
    }
}
