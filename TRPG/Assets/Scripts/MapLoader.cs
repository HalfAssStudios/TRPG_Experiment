using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

[System.Serializable]
public class MapContainer
{
    public string MapName;
    public int width;
    public int length;
    public int[] myGridTiles;

};

public class MapLoader : MonoBehaviour {

    string mapToLoad;
    string[] availableFiles;

    char[] splitters = new char[] { '/', '\\' };

   public MapContainer mapContainer;

    public List<Node> tileNodes = new List<Node>();

    void Start ()
    {
       availableFiles = Directory.GetFiles(Application.persistentDataPath);
	}
	
	void OnGUI () {
        int index = 0;
        foreach (string fileName in availableFiles)
        {
            Debug.Log(fileName);
            string[] splits = fileName.Split(splitters);

            if(GUI.Button(new Rect(50, 50 * index, 100, 50), splits[splits.Length - 1]))
            {
                LoadMap(fileName);

                AStar astar = GameObject.FindObjectOfType<AStar>();

                astar.Initialize(tileNodes, mapContainer.width, mapContainer.length);
            }
            index++;
        }
    }

    void LoadMap(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);

        mapContainer = (MapContainer)JsonUtility.FromJson(jsonData, typeof(MapContainer));
        //mapContainer = JsonMapper.ToObject<MapContainer>(jsonData);

        CreateGrid();
    }

    public void CreateGrid()
    {
        GameObject t_parent = new GameObject("Parent Object");
        GameObject t_go;
        for (int i = 0; i < mapContainer.myGridTiles.Length; i++)
        {
            if (mapContainer.myGridTiles[i] == 0) //Road
            {
                t_go = (GameObject)GameObject.Instantiate(Resources.Load("Tiles/Road"));
            }
            else if (mapContainer.myGridTiles[i] == 1) //River
            {
                t_go = (GameObject)GameObject.Instantiate(Resources.Load("Tiles/River"));
            }
            else if (mapContainer.myGridTiles[i] == 2) //Forest
            {
                t_go = (GameObject)GameObject.Instantiate(Resources.Load("Tiles/Forest"));
            }
            else if (mapContainer.myGridTiles[i] == 3) //Mountain
            {
                t_go = (GameObject)GameObject.Instantiate(Resources.Load("Tiles/Mount"));
            }
            else
            {
                t_go = (GameObject)GameObject.Instantiate(Resources.Load("Tiles/Road"));
            }

            int length = i % mapContainer.length;
            int width = (i / mapContainer.length) % mapContainer.width;

            t_go.transform.position = new Vector3(width, 0, length);
            t_go.transform.rotation = Quaternion.identity;
            t_go.transform.parent = t_parent.transform;

            tileNodes.Add(t_go.GetComponent<Node>());
            //t_go.transform.parent = t_rowParents.transform;
        }
    }
}
