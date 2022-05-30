using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MetalGamesSDK;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        DOTween.Kill(transform);
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce);
    }

    void Start()
    {
        transform.DORotate(transform.up, GameConfig.Instance.npcProperties.crownRotatoinTime)
            .SetLoops(-1, LoopType.Incremental);
    }
}