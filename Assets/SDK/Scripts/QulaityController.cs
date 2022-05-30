using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QulaityController : MonoBehaviour
{
    float avg = 0F; //declare this variable outside Update

    private void Update()
    {
        avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f;

        if (avg < 50)
        {
            QualitySettings.DecreaseLevel();
        }
        else if (avg<40)
        {
            QualitySettings.DecreaseLevel();
        }
    }
}