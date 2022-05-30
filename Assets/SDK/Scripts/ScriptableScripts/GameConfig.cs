using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace MetalGamesSDK
{
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
#if UNITY_EDITOR
        [OnInspectorGUI, PropertyOrder(0)]
        private void ShowImage()
        {
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/MuhammadTouseefSDK/Textures/GameConfig.png"),
                EditorStyles.centeredGreyMiniLabel);
        }

#endif


        //Inspector Properties

        [Space(15), TitleGroup("Properties")] [PropertyOrder(-1)]
        public Levels Levels;

        [PropertyOrder(-1)] public HUD HUD;


        public NpCProps npcProperties;
        public ObstacleProps obstacleProps;
    }


    [Serializable]
    public class Levels
    {
        public List<Level> LevelsPrefabs;
    }

    [Serializable]
    public class HUD
    {
        [Title("PunchRect() Punch Properties")]
        public Ease PunchEase;

        public float PunchTime;
        public float PunchScale;

        [Title("ShowScreen() Global Scale Properties")]
        public float Scale;

        public float ScaleTime;
        public Ease Tween;

        [Title("HUD Other Properties")] public float CurrencyImgScaleFactor;
        public float TapToStartScale;

        [Title("Settings Toggles Variables")] public float MoveFactor;
        public float ToggleTime;
    }

    [Serializable]
    public class LevelContainer
    {
        public Level Level;
    }

    [Serializable]
    public class PlayerProperties
    {
        public float speed;
        public float stoppingDistance;
        public float stopDelayOnCOunter;
    }

    [Serializable]
    public class PathdrawingConfigs
    {
        public float minPointsForWrongPathDetection;
    }


    [Serializable]
    public class NpCProps
    {
        public Material[] materials;
        public float crownRotatoinTime;
    }

    [Serializable]
    public class ObstacleProps
    {
        [Header("GlassProps")] public float timeToRotate;
        public float rotationAmount;
    }
}