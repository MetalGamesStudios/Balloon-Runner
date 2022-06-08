using System.Collections;
using System.Collections.Generic;
using C_;
using MetalGamesSDK;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public AnimancerController animancerController;
    [HideInInspector] public FiringController firingController;
    [SerializeField] public GameObject gunInHand, gunInSpine;
    public FeedBacker FeedBacker;
    
    private bool _gameStarted;
    public bool canMove;
    private bool _canRotate;

    private Transform tr;

    private float _currentX;
    private float _targetX;
    private float _targetZ;
    private float _forwardSpeed;
    private float _horizontalSpeed;
    public float mForwardSpeed
    {
        set { _forwardSpeed = value; }
        get { return _forwardSpeed; }
    }

    public float mHorizontalSpeed
    {
        set { _horizontalSpeed = value; }
        get { return _horizontalSpeed; }
    }

    [SerializeField] public PlayerMovementProps movementProps;
    [SerializeField] public RigBuilder rigBuilder;

    private void Awake()
    {
        tr = transform;

        _forwardSpeed = movementProps.playerForwardSpeed;
        _horizontalSpeed = movementProps.xSpeed - 1;

        animancerController = GetComponent<AnimancerController>();
        firingController = GetComponent<FiringController>();
        _targetX = transform.position.x;
        InvokeRepeating(nameof(SetRandomX), 1, Random.Range(2, 10));
    }

    private void Update()
    {
        UpdateHorizontalMoving();
        MoveForward();
        Rotate();
    }

    void SetRandomX()
    {
        if (!_gameStarted)
            return;
        var time = Random.Range(4, 10);
        StartCoroutine(setValue(time));
    }

    IEnumerator setValue(float time)
    {
        yield return new WaitForSeconds(time);
        _targetX = Random.Range(-movementProps.clampValue, movementProps.clampValue);
    }

    private void OnEnable()
    {
        GameManager.OnLeveLoadComplete += OnLevelLoadComplete;
        GameManager.OnLevelStarted += OnLevelStarted;
        firingController.Shoot += OnShoot;
        firingController.Unshoot += OnUnshoot;
        GameManagerBase.OnLevelCompleted += OnLevelComplete;
        GameManager.OnLevelFailed += OnLevelFailed;
        GameManager.OnReachingFinishLIne += OnReachingLine;
    }

    private void OnDisable()
    {
        GameManager.OnLeveLoadComplete -= OnLevelLoadComplete;
        GameManager.OnLevelStarted -= OnLevelStarted;
        firingController.Shoot -= OnShoot;
        firingController.Unshoot -= OnUnshoot;

        GameManagerBase.OnLevelCompleted -= OnLevelComplete;
        GameManager.OnLevelFailed -= OnLevelFailed;
        GameManager.OnReachingFinishLIne -= OnReachingLine;
    }

    void OnReachingLine()
    {
        animancerController.IdleAnimation();
        _forwardSpeed = 0;
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


    private void UpdateHorizontalMoving()
    {
        var heroZ = transform.localPosition.z;


        var maxOffsetX = movementProps.clampValue;


        _targetX = Mathf.Clamp(_targetX, -maxOffsetX, maxOffsetX);


        var maxDeltaX = _horizontalSpeed * Time.deltaTime;
        var lerpTargetX = Mathf.Lerp(_currentX, _targetX, movementProps.xLerp);
        _currentX = Mathf.MoveTowards(_currentX, lerpTargetX, maxDeltaX);
        SetPositionX(_currentX);
    }

    void SetPositionX(float x)
    {
        if (!canMove)
            return;
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
}