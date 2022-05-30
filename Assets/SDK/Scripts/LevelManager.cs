using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetalGamesSDK;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public Level currentLevel;
    public Scene currentLevelScene;
}