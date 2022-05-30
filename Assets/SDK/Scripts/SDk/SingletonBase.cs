using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MetalGamesSDK
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {

       protected virtual void Awake()
        {

        }

    }
}
