using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MetalGamesSDK;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public static Action onTouchDown;
    public static Action onTouchRelease;
    public static Action<float> onTouchMoved;
    public static Action OnStopMovingTouch;


    public bool ForceStartInput;
    private float _lastInputXposition;


    private void OnEnable()
    {
        GameManager.OnLevelStarted += () => Invoke(nameof(OnGameStarted), 0.1f);
        GameManager.OnLevelLoaded += () => State = GameState.none;
        GameManager.OnLevelCompleted += () => State = GameState.GameOver;
        GameManager.OnLevelFailed += () => State = GameState.GameOver;
    }

    enum GameState
    {
        none,
        GameStareted,
        GameOver
    }

    private GameState State;

    void OnGameStarted()
    {
        State = GameState.GameStareted;
    }


    void Update()
    {
        if (State != GameState.GameStareted && !ForceStartInput)
            return;
        TouchControls();
        EditorControls();
    }

    private Vector2 prevPos;

    private void TouchControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                onTouchDown?.Invoke();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                onTouchRelease?.Invoke();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
            }
        }
    }

    private void EditorControls()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            onTouchDown?.Invoke();
            _lastInputXposition = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))

        {
            onTouchRelease?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                onTouchMoved?.Invoke(Input.GetAxis("Mouse X"));
            }
            else
            {
                OnStopMovingTouch?.Invoke();
            }
        }
    }

    bool HasMouseMoved()
    {
        if (_lastInputXposition != Input.mousePosition.x)
        {
            Debug.Log($"Mouse is Still moving with delta {_lastInputXposition - Input.mousePosition.x}");
            return true;
        }

        return false;
    }
}