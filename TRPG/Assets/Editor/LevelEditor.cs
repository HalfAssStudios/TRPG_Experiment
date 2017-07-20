using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    static int m_gridLength = 10;
    static int m_gridWidth = 10;
    static List<List<int>> m_gridTiles = new List<List<int>>();

    #region Tiles
    GameObject m_Road;
    GameObject m_River;
    GameObject m_Forest;
    GameObject m_Mountain;
    #endregion //Tiles

    bool m_showGridSpaces = false;
    bool m_gridCreated = false;

    Vector2 m_scrollPosition = new Vector2(0, 0);

    Quaternion m_rotation = Quaternion.Euler(0, 0, 0);

    string m_editorMessage = "Welcome To Version 3";

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>(false, "Level Editor", true);
    }

    private void OnEnable()
    {
        m_Road = Resources.Load<GameObject>("Tiles/Road");
        m_River = Resources.Load<GameObject>("Tiles/River");
        m_Forest = Resources.Load<GameObject>("Tiles/Forest");
        m_Mountain = Resources.Load<GameObject>("Tiles/Mount");
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Level Editor Message: " + m_editorMessage);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Grid Length: ", EditorStyles.boldLabel);
        m_gridLength = EditorGUILayout.IntField(m_gridLength);
        EditorGUILayout.LabelField("Grid Width: ", EditorStyles.boldLabel);
        m_gridWidth = EditorGUILayout.IntField(m_gridWidth);

        if (m_gridLength <= 0 || m_gridLength > 100)
        {
            m_editorMessage = "Grid Length Cannot Be Below 0 or Above 200";
            m_gridLength = 1;
        }

        if (m_gridWidth <= 0 || m_gridWidth > 100)
        {
            m_editorMessage = "Grid Width Cannot Be Below 0 or Above 200";
            m_gridWidth = 1;
        }
        if ((m_gridWidth > 0 || m_gridWidth <= 200) && (m_gridLength > 0 || m_gridLength <= 200))
        {
            m_editorMessage = "Welcome To Version 3";
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Set Up Grid Tiles"))
        {

            SetGrid();
            m_showGridSpaces = true;
            m_gridCreated = true;
        }

        m_showGridSpaces = EditorGUILayout.Toggle("Show Grid Tiles", m_showGridSpaces);

        if (m_showGridSpaces)
        {
            EditorGUILayout.LabelField("Grid Tiles: Use 0 for Road Tiles, Use 1 for River Tiles");
            EditorGUILayout.LabelField("Grid Tiles: Use 2 for Forest Tiles, Use 3 for Mountain Tiles");

            EditorGUILayout.Space();

            ShowGrid();

            if (m_gridCreated)
                if (GUILayout.Button("Create Grid"))

                    CreateGrid();

            EditorGUILayout.Space();
        }

    }
    private void SetGrid()
    {
        int count = 0;
        Debug.Log(m_gridLength + "   " + m_gridWidth);
        m_gridTiles = new List<List<int>>();
        for (int i = 0; i < m_gridLength; i++)
        {
            m_gridTiles.Add(new List<int>());
            for (int j = 0; j < m_gridWidth; j++)
            {
                //m_gridTiles[i][j] = new int();
                m_gridTiles[i].Add(0);
                count++;
            }
        }
        Debug.Log(count);
    }

    private void ShowGrid()
    {
        m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

        EditorGUILayout.BeginHorizontal();//BeginHorizontal BeginVertical

        for (int i = 0; i < m_gridTiles.Count; i++)
        {
            EditorGUILayout.BeginVertical();
            for (int j = 0; j < m_gridTiles[i].Count; j++)
            {
                EditorGUILayout.LabelField("x:" + i + " y:" + j);
                m_gridTiles[i][j] = EditorGUILayout.IntField(m_gridTiles[i][j]);
            }
            EditorGUILayout.EndVertical(); //EndHorizontal
        }

        EditorGUILayout.EndHorizontal(); //EndVertical

        EditorGUILayout.EndScrollView();
    }

    private void CreateGrid()
    {
        CreateTiles();
    }

    private void CreateTiles()
    {
        GameObject t_go;
        for (int i = 0; i < m_gridTiles.Count; i++)
        {
            for (int j = 0; j < m_gridTiles[i].Count; j++)
            {
                if (m_gridTiles[i][j] == 0) //Road
                {
                    t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Road);
                    t_go.transform.position = new Vector3(i, 0, j);
                    t_go.transform.rotation = m_rotation;
                }
                else if (m_gridTiles[i][j] == 1) //River
                {
                    t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_River);
                    t_go.transform.position = new Vector3(i, 0, j);
                    t_go.transform.rotation = m_rotation;
                }
                else if (m_gridTiles[i][j] == 2) //Forest
                {
                    t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Forest);
                    t_go.transform.position = new Vector3(i, 0, j);
                    t_go.transform.rotation = m_rotation;
                }
                else if (m_gridTiles[i][j] == 3) //Mountain
                {
                    t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Mountain);
                    t_go.transform.position = new Vector3(i, 0, j);
                    t_go.transform.rotation = m_rotation;
                }
                else
                {
                    t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Road);
                    t_go.transform.position = new Vector3(i, 0, j);
                    t_go.transform.rotation = m_rotation;
                }
            }
        }
    }
}
