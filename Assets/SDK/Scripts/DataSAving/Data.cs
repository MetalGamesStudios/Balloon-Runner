using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int CurrentLevel;
    public int Currency;

    public Data(int i_Levelindex, int i_Currency)
    {
        CurrentLevel = i_Levelindex;
        Currency = i_Currency;
    }
}