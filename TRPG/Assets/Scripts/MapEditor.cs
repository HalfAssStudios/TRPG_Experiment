using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour {


    public GameObject emptyTilePrefab;

    public Vector2 maxMapDimensions = new Vector2(10f, 10f);
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < maxMapDimensions.x; i++)
        {
            for (int j = 0; j < maxMapDimensions.y; j++)
            {

                //GameObject.Instantiate(emptyTilePrefab, new Vector3( i, )
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
