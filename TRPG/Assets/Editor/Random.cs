using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Random : EditorWindow
{
    [MenuItem("My Game/Cheats")]
    public static void ShowWindow()
    {
        GetWindow<Random>(false, "Cheats", true);
    }

    void OnGUI()
    {
        EditorGUILayout.Toggle("Mute All Sounds", false);
        EditorGUILayout.IntField("Player Lifes", 3);
        EditorGUILayout.TextField("Player Two Name", "John");
    }
}
