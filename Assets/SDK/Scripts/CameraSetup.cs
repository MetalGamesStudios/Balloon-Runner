using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MetalGamesSDK;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraSetup : Singleton<CameraSetup>
{
    [SerializeField] private CinemachineVirtualCamera runningCam;
    [SerializeField] private CinemachineVirtualCamera currentCam;


    CinemachineBasicMultiChannelPerlin _perlin;

    private float _shakeTimer;
    private bool _gameStarted;

    private Vector3 _initialPos;

    public void SetPerlinFromCam(CinemachineVirtualCamera cm)
    {
        _perlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        _perlin = runningCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _initialPos = runningCam.transform.position;
        GameManager.OnLeveLoadComplete += OnLevelLoadComplete;

        GameManager.OnLevelStarted += OngameStarted;
        GameManager.OnLevelLoaded += OnGameLoad;
    }

    private void OnDisable()
    {
        GameManager.OnLeveLoadComplete -= OnLevelLoadComplete;

        GameManager.OnLevelStarted -= OngameStarted;

        GameManager.OnLevelLoaded -= OnGameLoad;
    }


    private void OngameStarted() => _gameStarted = true;

    void OnGameLoad()
    {
        _gameStarted = false;
        runningCam.transform.position = _initialPos;
    }

    private void Update()
    {
        if (!_gameStarted)
            return;

        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
            {
                _perlin.m_AmplitudeGain = 0;
                _perlin.m_FrequencyGain = 0;
            }
        }
    }

    public float shakeIntensity, shakeTime;

    [Button]
    public void ShakeCameraInvoker()
    {
        ShakeCamera(shakeIntensity, shakeTime, Vector3.zero);
    }

    void ShakeCamera(float i_intensity, float i_time, Vector3 i_pivot)
    {
        _perlin.m_AmplitudeGain = i_intensity;
        _shakeTimer = i_time;
        _perlin.m_FrequencyGain = 0.03f;
        _perlin.m_PivotOffset = i_pivot;
    }

    void OnLevelLoadComplete()
    {
        GetPlayerTransform();
    }

    void GetPlayerTransform()
    {
        var playertr = LevelManager.Instance.currentLevel.cameraTarget;
        runningCam.m_Follow = playertr;
        runningCam.LookAt = playertr;
    }


    public void SwitchToCam(CinemachineVirtualCamera cm)
    {
        currentCam.Priority = 0;
        cm.Priority = 1;
        currentCam = cm;
    }
}