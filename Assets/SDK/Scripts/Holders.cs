using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Holders : MonoBehaviour
{
    [Button]
    public void Pooltest()
    {
        PoolManager.Instance.PoolTest(Key);
    }

    [HorizontalGroup("Base"), LabelWidth(50), HideLabel]
    public ePoolType Key;

    [ReadOnly, HideInInspector] public GameObject Holder;

    private void Start()
    {
        int childCount = transform.childCount;
        for (int x = 0; x < childCount; x++)
        {
            if (Key == ePoolType.muzzle || Key == ePoolType.BalloonPop || Key == ePoolType.HitVFX)
                AvailableItems.Push(transform.GetChild(x).gameObject);
            else
            {
                availableItemsParticleSystems.Push(transform.GetChild(x).GetComponent<TrailRenderer>());
            }
        }
    }


    [BoxGroup("Base/Values"), LabelWidth(50)] [ReadOnly, ShowInInspector, PropertyOrder(3)]
    public Queue<GameObject> PooledItems = new Queue<GameObject>();

    [BoxGroup("Base/Values"), LabelWidth(50)] [ReadOnly, ShowInInspector, PropertyOrder(4)]
    public Stack<GameObject> AvailableItems = new Stack<GameObject>();

    [BoxGroup("Base/Values"), LabelWidth(50)] [ReadOnly, ShowInInspector, PropertyOrder(3)]
    public Queue<TrailRenderer> PooledParticleSystems = new Queue<TrailRenderer>();

    [BoxGroup("Base/Values"), LabelWidth(50)] [ReadOnly, ShowInInspector, PropertyOrder(4)]
    public Stack<TrailRenderer> availableItemsParticleSystems = new Stack<TrailRenderer>();
}