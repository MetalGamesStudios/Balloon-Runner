using System;
using System.Collections;
using System.Collections.Generic;
using MetalGamesSDK;
using UnityEngine;

public class FiringController : MonoBehaviour
{
    [SerializeField] private Transform instantiationPoint;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform firingOrigin;

    [SerializeField] private AnimancerController _animancerController;
    [SerializeField] public Crown crown;

    [Tooltip("Firing Bullets / second"), Header("Firing Bullets / second")] [SerializeField]
    private float firingRate;

    [SerializeField] private LayerMask balloonMask;
    [SerializeField] private float _timeToShoot;
    [SerializeField] private float timeStamp;

    [SerializeField] private bool canFire = true;
    [SerializeField] public bool canShoot = true;
    
    private bool _gameStarted;

    private Vector3 _offsetFire;

    private PoolManager _poolManager;

    public Action Shoot, Unshoot;

    private void Awake()
    {
        _poolManager = PoolManager.Instance;

        _offsetFire = Vector3.one * 0.5f;
    }

    private void OnEnable()
    {
        GameManager.OnLevelStarted += OnLevelStarted;
        GameManager.OnLeveLoadComplete += OnLevelLoaded;
    }

    private void OnDisable()
    {
        GameManager.OnLevelStarted -= OnLevelStarted;
        GameManager.OnLeveLoadComplete -= OnLevelLoaded;
    }

    void OnLevelStarted() => _gameStarted = true;
    void OnLevelLoaded() => _gameStarted = false;

    private void Update()
    {
        if (!_gameStarted)
            return;

        var initailpos = firingOrigin.position;

        Ray ra = new Ray(firingOrigin.position, (targetTransform.position - initailpos) * 100);

        Debug.DrawRay(ra.origin, ra.direction * 100, Color.blue, 0.001f);
        if (Physics.Raycast(ra, 100, balloonMask))
        {
            if (!canShoot)
            {
                Shoot?.Invoke();
                canShoot = true;
            }
        }
        else if (canShoot)
        {
            Unshoot?.Invoke();
            canShoot = false;
        }

        if (!canShoot)
            return;
        if (canFire)
        {
            canFire = false;
            var timetoAdd = 1 / firingRate;
            timeStamp = Time.time + timetoAdd;
            InstantiateFire();
        }
        else if (Time.time > timeStamp)
        {
            canFire = true;
        }
    }


    void InstantiateFire()
    {
        Vector3 targetRandomPos = Vector3.zero;

        ePoolType targetPoolType = ePoolType.Fire;
        switch (type)
        {
            case FireType.Fire:
                targetPoolType = ePoolType.Fire;
                break;
            case FireType.Fire1:
                targetPoolType = ePoolType.Fire1;
                break;
            case FireType.Fire2:
                targetPoolType = ePoolType.Fire2;
                break;
        }


        _poolManager.PoolTrailItem(targetPoolType, instantiationPoint.position, instantiationPoint.rotation);

        _poolManager.PoolItem(ePoolType.muzzle, instantiationPoint.position);
    }


    public enum FireType
    {
        Fire,
        Fire1,
        Fire2
    }

    public FireType type;
}