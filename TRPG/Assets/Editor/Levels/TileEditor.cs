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

        if (m_selectedObjects == null)
            return;

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
            if(t_obj.GetComponent<Node>() != null)
            {
                t_transform = t_obj.transform;

                switch (i_type)
                {
                    case TileTypes.ROAD:
                        {
                            t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Road);
                            break;
                        }
                    case TileTypes.RIVER:
                        {
                            t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_River);
                            break;
                        }
                    case TileTypes.FOREST:
                        {
                            t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Forest);
                            break;
                        }
                    case TileTypes.MOUNTAIN:
                        {
                            t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Mountain);
                            break;
                        }
                    default:
                        {
                            Debug.Log("You done fucked up.");
                            t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_Road);
                            break;
                        }
                }

                t_go.transform.position = t_transform.position;
                t_go.transform.rotation = t_transform.rotation;
                t_go.transform.parent = t_transform.parent;

                //grab parent.
                for(int i = 0; i <  t_transform.parent.childCount; i++)
                {
                    //iterate through childern
                    if(t_transform.parent.GetChild(i).gameObject == t_obj)
                    {//compare till found
                     //set grid index to child index 
                     //i = row * 4 + col
                     //p.x = index / 3;
                     //p.y = index % 3;
                        int mapL = t_transform.parent.GetComponent<MapSaver>().length;
                        int mapW = t_transform.parent.GetComponent<MapSaver>().width;

                        int length = i % mapL;
                        int width = (i / mapL) % mapW;

                        List <List<int>> grids = t_transform.parent.GetComponent<MapSaver>().m_gridTiles;
                        t_transform.parent.GetComponent<MapSaver>().m_gridTiles[length][width] = (int)i_type;
                        t_go.transform.SetSiblingIndex(i);
                        DestroyImmediate(t_obj);
                        break;
                    }
                }

            }
            else
            {
                Debug.Log("Niggah watcha tryna do? where yo node at tho?");
            }
        }
    }

    void Update()
    {
        m_selectedObject = Selection.activeGameObject;
        m_selectedObjects = Selection.gameObjects;
    }
}

