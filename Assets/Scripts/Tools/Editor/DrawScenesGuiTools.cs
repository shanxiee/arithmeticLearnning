using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using System;

public class DrawScenesGuiTools : Editor {

    [InitializeOnLoadMethod]
    private static  void PackageLuaAndStartScenes()
    {

        SceneView.onSceneGUIDelegate += delegate(SceneView sceneView)
        {
            if (!Application.isPlaying)
            {
                Handles.BeginGUI();

                if (GUI.Button(new Rect(Screen.width - 150f, 50f, 100, 20f), "打包Lua后启动"))
                {
                    Action playScenes = delegate
                    {
                        EditorApplication.isPlaying = true;
                    };  
                }
                Handles.EndGUI();
            }

        };
    }

}
