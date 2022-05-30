using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObsButton : MonoBehaviour
{
    public float selfY;
    public GameObject sawToBringUp;
    public float sawMovementValue;
    public float sawHorizontalYPosition;
    public float timeForHorizontalMovement;
    [SerializeField] private Material transparentMat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("box"))
        {
            Press();
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Renderer>().material = transparentMat;
        }
    }

    void Press()
    {
        transform.DOLocalMoveZ(selfY, 0.1f);
        sawToBringUp.gameObject.SetActive(true);
        sawToBringUp.transform.DOLocalMoveZ(sawMovementValue, 0.4f).SetEase(Ease.OutBounce);
        sawToBringUp.transform.DOLocalRotate(Vector3.right * 100, 0.3f).SetLoops(-1, LoopType.Incremental);
        sawToBringUp.transform.DOLocalMoveY(sawHorizontalYPosition, timeForHorizontalMovement)
            .SetLoops(-1, LoopType.Yoyo);
    }
}