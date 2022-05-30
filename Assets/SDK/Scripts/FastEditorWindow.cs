#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class FastEditorWindow : EditorWindow
{
    private static EditorWindow window;

    [MenuItem("MetalGamesSDK/Open GameConfigurations #g")]
    public static void SelectGameConfig()
    {
        var GM = Resources.Load("GameConfig");
        Selection.activeObject = GM;
    }

    [MenuItem("MetalGamesSDK/Select StoreManager #s")]
    public static void SelectStoreManager()
    {
        Selection.activeGameObject = StorageManager.Instance.gameObject;
    }

    [MenuItem("MetalGamesSDK/Select PoolManager #p")]
    public static void SelectPoolManager()
    {
        Selection.activeGameObject = PoolManager.Instance.gameObject;
    }

    [MenuItem("MetalGamesSDK/Open ThemeManager #t")]
    public static void OpenThemeManager()
    {
        // var GM = Resources.Load("ThemeManager");
        Selection.activeObject = ThemeManagerMONO.Instance.gameObject;
    }

    [MenuItem("MetalGamesSDK/Apply Level Prefab #a")]
    public static void ApplyCurrentLevelPrefab()
    {
        LevelManager.Instance.currentLevel.ApplyThisPrrefab();
    }
}
#endif