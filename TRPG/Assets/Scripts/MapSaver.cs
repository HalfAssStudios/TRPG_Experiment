using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using LitJson;

[System.Serializable]
public class MapSaver : MonoBehaviour
{
    #region Stats
    public string MapName = "RENAMEME";
    public int width, length;
    public List<List<int>> m_gridTiles = new List<List<int>>();
    public int[] myGridTiles;
    #endregion

    public void ConvertGrid()
    {
        myGridTiles = new int[width * length];
        int arrIter = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                myGridTiles[arrIter] = m_gridTiles[i][j];
                arrIter++;
            }
        }
    }

    private void WriteJsonFile()
    {
        string mapString = JsonUtility.ToJson(this, false);

        //MapContainer map = new MapContainer();

        //map.width = width;
        //map.length = length;
        //map.array = new int[width * length];
        //map.array = myGridTiles;

        //string mapString = JsonMapper.ToJson(map);

        if (!string.IsNullOrEmpty(mapString))
        {
            int iter = 0;
            while (File.Exists(Application.persistentDataPath + "/" + MapName + ".json") || File.Exists(Application.persistentDataPath + "/" + MapName + iter + ".json"))
            {
                iter++;
            }

            File.WriteAllText(Application.persistentDataPath + "/" + MapName + iter + ".json", mapString);
        }
        else
            Debug.Log("Get cho mind right");

    }

    public void SaveMap()
    {
        ConvertGrid();
        WriteJsonFile();
    }
}