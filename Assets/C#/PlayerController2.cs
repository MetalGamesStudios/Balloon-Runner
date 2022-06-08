using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using C_;
using MetalGamesSDK;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController2 : Singleton<PlayerController2>{
    [HideInInspector] public AnimancerController animancerController;
    [HideInInspector] public FiringController firingController;


    private bool _gameStarted;
    public bool canMove;


    private Transform tr;

    private float _currentX;
    private float _targetX;
    private float _targetZ;
    public float _forwardSpeed;

    [SerializeField] public PlayerMovementProps movementProps;
    public RigBuilder rigBuilder;
    
    [SerializeField] private GameObject gunInHand, gunInSpine;
    [SerializeField] public FeedBacker _feedBacker;
    private bool _canRotate = true;

    private void Awake()
    {
        tr = transform;

        animancerController = GetComponent<AnimancerController>();
        firingController = GetComponent<FiringController>();
        _forwardSpeed = movementProps.playerForwardSpeed;
    }

    private void Update()
    {
        UpdateHorizontalMoving();
        MoveForward();

        Rotate();
    }

    private void OnEnable()
    {
        InputController.onTouchDown += OnTouchBegan;
        GameManager.OnLeveLoadComplete += OnLevelLoadComplete;
        GameManager.OnLevelStarted += OnLevelStarted;
        GameManagerBase.OnLevelCompleted += OnLevelComplete;
        GameManagerBase.OnLevelFailed += OnLevelFailed;

        firingController.Shoot += OnShoot;
        firingController.Unshoot += OnUnshoot;
    }

    private void OnDisable()
    {
        InputController.onTouchDown -= OnTouchBegan;
        GameManager.OnLeveLoadComplete -= OnLevelLoadComplete;
        GameManager.OnLevelStarted -= OnLevelStarted;
        firingController.Shoot -= OnShoot;
        firingController.Unshoot -= OnUnshoot;
        GameManagerBase.OnLevelCompleted -= OnLevelComplete;
        GameManagerBase.OnLevelFailed -= OnLevelFailed;
    }

    void OnLevelLoadComplete()
    {
        canMove = true;
        _gameStarted = false;
    }

    void OnLevelStarted()
    {
        _gameStarted = true;
    }


    void OnTouchBegan()
    {
        StartCoroutine(HorizontalMoveCoroutine());
    }

    private bool HasTouch()
    {
        return Input.GetMouseButton(0) || Input.touchCount != 0;
    }

    private IEnumerator HorizontalMoveCoroutine()
    {
        var prevTouchPosition = GetWorldTouchPosition();

        while (HasTouch())
        {
            if (canMove)
            {
                var touchPosition = GetWorldTouchPosition();
                _targetX += (touchPosition.x - prevTouchPosition.x) * movementProps.dragSensitivity;


                //Stop Firing Based On Horizontal Movement


                prevTouchPosition = touchPosition;
            }

            yield return null;
        }

        _targetX = transform.localPosition.x;
    }

    private void UpdateHorizontalMoving()
    {
        var heroZ = transform.localPosition.z;


        var maxOffsetX = movementProps.clampValue;


        _targetX = Mathf.Clamp(_targetX, -maxOffsetX, maxOffsetX);


        var maxDeltaX = movementProps.xSpeed * Time.deltaTime;
        var lerpTargetX = Mathf.Lerp(_currentX, _targetX, movementProps.xLerp);

        _currentX = Mathf.MoveTowards(_currentX, lerpTargetX, maxDeltaX);
        SetPositionX(_currentX);
    }

    void SetPositionX(float x)
    {
        var targetpos = transform.position + new Vector3(x, 0, 0);
        tr.position = new Vector3(x, targetpos.y, targetpos.z);
    }

    void MoveForward()
    {
        if (_gameStarted && canMove)
        {
            _targetZ = Time.deltaTime * _forwardSpeed;
            transform.position += new Vector3(0, 0, _targetZ);
        }
    }


    private float _prevPosition;
    private float _targetRotation;
    private float _currentRotation;

    void Rotate()
    {
        var maxOffset = movementProps.yMaxOffset;
        var positionX = transform.position.x;
        var delta = Mathf.Clamp(positionX - _prevPosition, -maxOffset, maxOffset);

        if (_canRotate)
            _targetRotation = delta / maxOffset * movementProps.yMaxRotation;
        else
            _targetRotation = 0;
        _prevPosition = positionX;

        /* if (!canRotate)
         {
             _targetRotation = 0;
         }*/

        if (_currentRotation != _targetRotation)
        {
            _currentRotation = Mathf.LerpAngle(_currentRotation, _targetRotation, movementProps.yRotationTime);
            SetRotationY(_currentRotation);
        }
    }

    public void SetRotationY(float y)
    {
        var rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rotation.x, y, rotation.z);
    }

    void OnShoot()
    {
        animancerController.GrabGun();
        gunInHand.SetActive(true);
        gunInSpine.SetActive(false);
        rigBuilder.enabled = true;
        _canRotate = false;
    }

    void OnUnshoot()
    {
        animancerController.PutBackGun();
        gunInHand.SetActive(false);
        gunInSpine.SetActive(true);
        rigBuilder.enabled = false;
        _canRotate = true;
    }

    void OnLevelFailed()
    {
        animancerController.DefeatAnimation();
        canMove = false;
    }

    void OnLevelComplete()
    {
        animancerController.WinAnimation();
        canMove = false;
    }
#if UNITY_EDITOR
    Vector3 GetWorldTouchPosition()
    {
        return Input.mousePosition;
    }
#else
Vector3 GetWorldTouchPosition()
{
   return Input.GetTouch(0).position;

}
#endif
}