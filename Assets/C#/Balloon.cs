using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace C_
{
    public class Balloon : MonoBehaviour
    {
        [SerializeField] private BalloonManager manager;
        [SerializeField] private SphereCollider sphereCollider;

        [Button]
        public void GetReferences()
        {
            manager = GetComponentInParent<BalloonManager>();
            sphereCollider = GetComponent<SphereCollider>();
        }

        public void DisableBallon()
        {
            GetComponent<Collider>().enabled = false;
            AudioManager.instance.PlayRandomPopSound();

            HapticsManager.Instance.SmallPop();
            transform.DOScale(transform.lossyScale * 1.5f, 0.1f).OnComplete(delegate
            {
                gameObject.SetActive(false);
                PoolManager.Instance.PoolItem(ePoolType.BalloonPop, transform.position);
            });
        }
    }
}