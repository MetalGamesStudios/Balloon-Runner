using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MetalGamesSDK
{
    public class GameManagerBase : Singleton<GameManager>
    {
        public delegate void GameEvents();

     

        public delegate void CurrencyEvent(int Currency);
        //Events

        public static event GameEvents OnLevelLoaded;
        public static event GameEvents OnLeveLoadComplete;
        public static event GameEvents OnLevelStarted;
        public static event GameEvents OnLevelCompleted;
        public static event GameEvents OnLevelFailed;
        public static event GameEvents ResetLvl;

        public static event CurrencyEvent OnCurrencyChanged;


        public virtual void LoadLevel()
        {
            OnLevelLoaded?.Invoke();
        }

        public virtual void StartLevel()
        {
            OnLevelStarted?.Invoke();
        }

        public virtual void OnLeveLoadCompleted()
        {
            OnLeveLoadComplete?.Invoke();
        }

        public virtual void LevelComplete()
        {
            OnLevelCompleted?.Invoke();
          
        }

        public virtual void LevelFail()
        {
            OnLevelFailed?.Invoke();
        }

        public virtual void ResetLevel()
        {
            ResetLvl?.Invoke();
        }

        public virtual void CurrencyChanged(int Currency)
        {
            OnCurrencyChanged?.Invoke(Currency);
        }

        #region Shortcut Methods

        #endregion
    }
}