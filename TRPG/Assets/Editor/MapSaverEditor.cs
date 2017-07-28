using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapSaver))]
public class MapSaverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapSaver mapSaver = (MapSaver)target;
        if(GUILayout.Button("Convert Grid"))
        {
            mapSaver.ConvertGrid();
        }

        if (GUILayout.Button("Save Map"))
        {
            mapSaver.SaveMap();
        }

        base.OnInspectorGUI();
    }
}
