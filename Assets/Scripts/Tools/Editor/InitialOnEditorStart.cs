using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InitialOnEditorStart : Editor {

    [InitializeOnLoadMethod]
    static void InitializeSetAutoLoadedLua()
    {
        var flag = EditorPrefs.GetBool("autoPackageLua");
        if (flag)
        {
            AutoPackageLua(true);
        }
    }

    const string packageLuaPath = "Tools/自动打包Lua";
    [MenuItem(packageLuaPath)]
    static void SetAutoPackageLua()
    {
        var flag = Menu.GetChecked(packageLuaPath);
        EditorPrefs.SetBool("autoPackageLua", !flag);
        AutoPackageLua(!flag);
    }

    [MenuItem(packageLuaPath, true)]
    public static bool SetAutoPackageLuaPre()
    {
        var flag = EditorPrefs.GetBool("autoPackageLua");
        Menu.SetChecked(packageLuaPath, flag);
        return true;
    }

    static void AutoPackageLua(bool stateChange)
    {

        Action<PlayModeStateChange> aotuLoadLua = delegate (PlayModeStateChange stateChagne)
        {
            if (stateChagne == PlayModeStateChange.EnteredPlayMode)
            {
                //Debug.Log("AutoLoadLua Star");
            }
        };

        if (stateChange == true)
        {
            EditorApplication.playModeStateChanged += aotuLoadLua;
            //Debug.Log("Add AutoLoadedLua Event");
        }
        else
        {
            EditorApplication.playModeStateChanged -= aotuLoadLua;
            //Debug.Log("Remove AutoLoadedLua Event");
        }

    }
}
