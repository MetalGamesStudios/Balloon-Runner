using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalGamesSDK
{
    public class Singleton<T> : SingletonBase<Singleton<T>> where T : MonoBehaviour
    {
        private static T s_instance;
        bool m_IsSingletonLogsEnabled = false;

        public bool IsSingletonLogsEnabled { get => m_IsSingletonLogsEnabled; set => m_IsSingletonLogsEnabled = value; }
        public static T Instance
        {
            get
            {
                s_instance = (T)FindObjectOfType<T>();


                if (s_instance == null)
                {
                    GameObject NewObj = new GameObject();
                    s_instance = NewObj.AddComponent<T>();
                    NewObj.name = "Runtime_Singleton " + typeof(T).ToString();

                }

                return s_instance;
            }
        }

        protected sealed override void Awake()
        {
            if (s_instance == null)
            {
                s_instance = gameObject.GetComponent<T>();
                // setDontDestroyOnLoad();
            }
        }


        protected void setDontDestroyOnLoad()
        {
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}