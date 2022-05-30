using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using MK.Toon;

public class PoolManager : PoolManagerBase
{
    [PropertyOrder(99), PropertySpace(10)] public float DequeueDelay;

    public void PoolTest(ePoolType ItemToTest)
    {
        foreach (HolderData holder in HoldersInfo)
        {
            if (holder.HolderScript.Key == ItemToTest)
            {
                Holders holderScript = holder.HolderScript;

                GameObject prefab = GetPrefab(holderScript);

                if (prefab != null)
                {
                    DOVirtual.DelayedCall(DequeueDelay, delegate()
                    {
                        GameObject prefab = holderScript.PooledItems.Dequeue();
                        holderScript.AvailableItems.Push(prefab);
                    });
                }
            }
        }
    }

    public void PoolItem(ePoolType i_ePoolType, Vector3 i_Position)
    {
        foreach (HolderData holder in HoldersInfo)
        {
            if (holder.HolderScript.Key == i_ePoolType)
            {
                Holders holderScript = holder.HolderScript;

                GameObject prefab = GetPrefab(holderScript);

                StartCoroutine(DequewTween(holderScript));

                if (prefab != null)
                {
                    prefab.transform.position = i_Position;
                    prefab.SetActive(true);
                }
            }
        }
    }


    IEnumerator DequewTween(Holders holderScript)
    {
        yield return new WaitForSeconds(DequeueDelay);

        GameObject prefab = holderScript.PooledItems.Dequeue();

        prefab.SetActive(false);
        prefab.transform.position = Vector3.zero;
        holderScript.AvailableItems.Push(prefab);
    }

    IEnumerator DequewTweenTrail(Holders holderScript)
    {
        yield return new WaitForSeconds(DequeueDelay);

        var prefab = holderScript.PooledParticleSystems.Dequeue();

        prefab.gameObject.SetActive(false);
        prefab.transform.position = Vector3.zero;
        holderScript.availableItemsParticleSystems.Push(prefab);
    }

    public void PoolTrailItem(ePoolType i_ePoolType, Vector3 i_Position)
    {
        foreach (HolderData holder in HoldersInfo)
        {
            if (holder.HolderScript.Key == i_ePoolType)
            {
                Holders holderScript = holder.HolderScript;

                var prefab = getTrailTrPrefab(holderScript);

                StartCoroutine(DequewTween(holderScript));

                if (prefab != null)
                {
                    prefab.gm.transform.position = i_Position;
                    prefab.gm.SetActive(true);
                }
            }
        }
    }

    public void PoolTrailItem(ePoolType i_ePoolType, Vector3 i_Position, Quaternion i_Rotation)
    {
        foreach (HolderData holder in HoldersInfo)
        {
            if (holder.HolderScript.Key == i_ePoolType)
            {
                Holders holderScript = holder.HolderScript;

                var prefab = getTrailTrPrefab(holderScript);

                prefab.renderer.enabled = false;
                StartCoroutine(DequewTweenTrail(holderScript));

                if (prefab != null)
                {
                    prefab.gm.transform.position = i_Position;
                    prefab.gm.transform.rotation = i_Rotation;
                    prefab.gm.SetActive(true);
                }
            }
        }
    }


    public void PoolItem(ePoolType i_ePoolType, Vector3 i_Position, Quaternion i_rotation)
    {
        foreach (HolderData holder in HoldersInfo)
        {
            if (holder.HolderScript.Key == i_ePoolType)
            {
                Holders holderScript = holder.HolderScript;

                GameObject prefab = GetPrefab(holderScript);


                if (prefab != null)
                {
                    prefab.transform.position = i_Position;
                    prefab.transform.rotation = i_rotation;
                    prefab.SetActive(true);

                    StartCoroutine(DequewTween(holderScript));
                }
            }
        }
    }

    CustomTrailTr getTrailTrPrefab(Holders i_HolderScript)
    {
        if (i_HolderScript.availableItemsParticleSystems.Count > 0)
        {
            var renderer = i_HolderScript.availableItemsParticleSystems.Pop();
            i_HolderScript.PooledParticleSystems.Enqueue(renderer);

            CustomTrailTr data = new CustomTrailTr();
            data.renderer = renderer;
            data.gm = renderer.gameObject;

            return data;
        }
        else
            Debug.LogError(
                "[PoolManager]=>  No More Prefabs Available Decrese delay Time or Increase Initially instantiated prefabs ");

        return null;
    }


    GameObject GetPrefab(Holders i_HolderScript)
    {
        if (i_HolderScript.AvailableItems.Count > 0)
        {
            GameObject prefab = i_HolderScript.AvailableItems.Pop();
            i_HolderScript.PooledItems.Enqueue(prefab);
            return prefab;
        }


        else
            Debug.LogError(
                "[PoolManager]=>  No More Prefabs Available Decrese delay Time or Increase Initially instantiated prefabs ");

        return null;
    }

    public class CustomTrailTr
    {
        public GameObject gm;
        public TrailRenderer renderer;
    }
}