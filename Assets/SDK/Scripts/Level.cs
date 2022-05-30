using DG.Tweening;
using MetalGamesSDK;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


[DefaultExecutionOrder(-10)]
public class Level : MonoBehaviour
{
    public Transform cameraTarget;


    private void OnEnable()
    {
    }

    private void Awake()
    {
        LevelManager.Instance.currentLevel = this;
    }


    private void Start()
    {
        InitLevel();
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }


    void InitLevel()
    {
        GameManager.Instance.OnLeveLoadCompleted();
    }

    [Button]
    public void SetThisAsCurrentLevel()
    {
        LevelManager.Instance.currentLevel = this;
    }

    [Button]
    public void ApplyThisPrrefab()
    {
#if UNITY_EDITOR


        try
        {
            PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
            Debug.Log("Prefab Applied Succesfully !!");
        }
        catch
        {
            Debug.LogError("Current Level Prefab Not Set");
        }
#endif
    }
}