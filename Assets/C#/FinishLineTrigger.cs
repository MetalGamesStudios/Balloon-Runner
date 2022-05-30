using System;
using System.Collections;
using System.Collections.Generic;
using MetalGamesSDK;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    private bool _gameOVer;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement mv))
        {
            if (!_gameOVer)
                GameManager.Instance.LevelFail();
            _gameOVer = true;
        }
        else if (other.TryGetComponent(out PlayerController2 cm))
        {
            GameManager.Instance.LevelComplete();
        }
    }
}