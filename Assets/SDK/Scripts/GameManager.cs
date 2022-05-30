using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace MetalGamesSDK
{
    public class GameManager : GameManagerBase
    {
        public static Action OnReachingNextArena;

        private void Start()
        {
            LoadLevel();
            
        }

        public override void StartLevel()
        {
            base.StartLevel();
        }

        public override void LoadLevel()
        {
            DOTween.KillAll();
            if (LevelManager.Instance.currentLevel)
                LevelManager.Instance.currentLevel.SelfDestruct();

            int levelToLoad = (StorageManager.Instance.CurrentLevel)
                              % GameConfig.Instance.Levels.LevelsPrefabs.Count;
            Debug.Log($"loading level {levelToLoad}");


            LevelManager.Instance.currentLevel = Instantiate(GameConfig.Instance.Levels.LevelsPrefabs[levelToLoad]);


            //  SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
            base.LoadLevel();
          
        }

        public void RestartLevel()
        {
            ResetLevel();
            LoadLevel();
            Debug.Log("level Reset");
        }

        public override void OnLeveLoadCompleted()
        {
            base.OnLeveLoadCompleted();
        }

        public override void ResetLevel()
        {
            base.ResetLevel();
        }

        public void NextLevel()
        {
            StorageManager.Instance.CurrentLevel++;
            LoadLevel();
            
        }

        public void Previouslevel()
        {
            if (StorageManager.Instance.CurrentLevel > 1)
            {
                StorageManager.Instance.CurrentLevel--;
                LoadLevel();
            }
        }

#if UNITY_EDITOR
        private void LateUpdate()
        {
            var key = Input.inputString;
            if (key != "")
            {
                switch (key)
                {
                    case "p":
                        Previouslevel();
                        Debug.Log("Previouslevel");
                        break;
                    case "r":
                        ResetLevel();
                        RestartLevel();
                        break;
                    case "n":
                        NextLevel();
                        Debug.Log("NextLevel");
                        break;
                    case "1":
                        LevelComplete();
                        Debug.Log("levelCompete");
                        break;
                    case "2":
                        base.LevelFail();
                        Debug.Log("levelFailed");
                        break;
                    case "s":
                        StartLevel();
                        break;
                }
            }
        }
#endif


        #region Learning

        // void SortedList()
        // {
        //     SortedList<string, int> sortlist = new SortedList<string, int>();
        //     sortlist.Add("11", 100);
        //     sortlist.Add("2", 200);
        //     sortlist.Add("1", 300);

        //     foreach (string key in sortlist.Keys)
        //     {
        //         Debug.Log("key is " + key + "Value is " + sortlist[key]);
        //     }

        // }


        // void SwapNumbers<s>(ref s a, ref s b)
        // {
        //     s temp = a;
        //     a = b;
        //     b = temp;
        //     Debug.Log("First Value is =" + a + " Second Value is  = " + b);
        // }
        // class Stack<T>
        // {
        //     int index = 0;
        //     T[] innerArray = new T[100];
        //     public void Push(T item)
        //     {
        //         innerArray[index++] = item;
        //     }
        //     public T Pop()
        //     {
        //         return innerArray[--index];
        //     }
        //     public T Get(int k) { return innerArray[k]; }
        // }

        #endregion
    }
}