#if UNITY_EDITOR
using UnityEditor;
 using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
 [InitializeOnLoad]
 public static class EditorHotkeysTracker
 {
   
    static EditorHotkeysTracker()
     {
         SceneView.duringSceneGui += view =>
         {
if(!PlayerPrefs.HasKey("e"))
{
    
}

            //  var e = Event.current;
            //  if (e != null && e.keyCode != KeyCode.None)
            //      //Debug.Log("Key pressed in editor: " + e.keyCode);

            //  if (e.keyCode == KeyCode.LeftControl )
            //  {
//                  var a = Resources.Load("GameConfig");
//                  Selection.activeObject = a;
// Debug.Log("Selection is From Editor hotkeystracker onscenegui");
             
         };
    }
}
#endif
