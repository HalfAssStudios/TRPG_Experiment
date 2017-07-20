using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileEditor : EditorWindow
{
    [MenuItem("Tools/Tile Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileEditor>(false, "Tile Editor", true);
    }
    enum TileTypes { ROAD, RIVER, FOREST, MOUNTAIN };
    Vector2 m_scrollPosition = new Vector2(0, 0);

    static GameObject m_selectedObject;
    static GameObject[] m_selectedObjects;

    #region Tiles
    GameObject m_Road;
    GameObject m_River;
    GameObject m_Forest;
    GameObject m_Mountain;
    #endregion //Tiles

    private void OnEnable()
    {
        m_Road = Resources.Load<GameObject>("Tiles/Road");
        m_River = Resources.Load<GameObject>("Tiles/River");
        m_Forest = Resources.Load<GameObject>("Tiles/Forest");
        m_Mountain = Resources.Load<GameObject>("Tiles/Mount");
    }

    void OnGUI()
    {
        m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);

        if (m_selectedObjects.Length > 0)
        {
            GUILayout.Label("MultiSelection");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Road"))
                SwapTiles(TileTypes.ROAD);
            if (GUILayout.Button("River"))
                SwapTiles(TileTypes.RIVER);
            if (GUILayout.Button("Forest"))
                SwapTiles(TileTypes.FOREST);
            if (GUILayout.Button("Mountain"))
                SwapTiles(TileTypes.MOUNTAIN);
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label("Swapper: Please Select A Tile or A Group of Tiles");
        }

        GUILayout.EndScrollView();
    }

    void RotateClockWise(float i_degrees)
    {
        foreach (GameObject t_go in m_selectedObjects)
        {
            t_go.transform.Rotate(0, i_degrees, 0);
        }
    }

    void RotateCounterClockWise(float i_degrees)
    {
        foreach (GameObject t_go in m_selectedObjects)
        {
            t_go.transform.Rotate(0, i_degrees, 0);
        }
    }

    void SwapTiles(TileTypes i_type)
    {
        GameObject t_go;
        Transform t_transform;

        foreach (GameObject t_obj in m_selectedObjects)
        {
            t_transform = t_obj.transform;

            switch (i_type)
            {
                case TileTypes.ROAD:
                    {
                        t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Road);
                        t_go.transform.position = t_transform.position;
                        t_go.transform.rotation = t_transform.rotation;
                        break;
                    }
                case TileTypes.RIVER:
                    {
                        t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_River);
                        t_go.transform.position = t_transform.position;
                        t_go.transform.rotation = t_transform.rotation;
                        break;
                    }
                case TileTypes.FOREST:
                    {
                        t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Forest);
                        t_go.transform.position = t_transform.position;
                        t_go.transform.rotation = t_transform.rotation;
                        break;
                    }
                case TileTypes.MOUNTAIN:
                    {
                        t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Mountain);
                        t_go.transform.position = t_transform.position;
                        t_go.transform.rotation = t_transform.rotation;
                        break;
                    }
            }

            DestroyImmediate(t_obj);
        }
    }

    void Update()
    {
        m_selectedObject = Selection.activeGameObject;
        m_selectedObjects = Selection.gameObjects;
    }
}

