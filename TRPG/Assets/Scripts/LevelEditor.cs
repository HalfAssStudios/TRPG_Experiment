using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : EditorWindow
{
	static int m_gridLength = 10;
	static int m_gridWidth = 10;
	static List<List<int>> m_gridTiles = new List<List<int>>();

	#region Tiles
	GameObject m_GlowDeathTile = Resources.Load<GameObject>("EdgeGlow/Death_02");
	GameObject m_GlowOutlineTile = Resources.Load<GameObject>("EdgeGlow/Outline_02");
	GameObject m_GlowBoosterTile = Resources.Load<GameObject>("EdgeGlow/Booster_02");
	GameObject m_GlowPusherTile = Resources.Load<GameObject>("EdgeGlow/Pusher_02");
	GameObject m_GlowTeleporterTile = Resources.Load<GameObject>("EdgeGlow/Teleporter_02");    
	GameObject m_GlowMineTile = Resources.Load<GameObject>("EdgeGlow/Mine_02");
	GameObject m_GlowRotatorTile = Resources.Load<GameObject>("EdgeGlow/Rotator_02");
	GameObject m_GlowSolidTile = Resources.Load<GameObject>("EdgeGlow/Solid_02");
	GameObject m_GlowSpeedStartTile = Resources.Load<GameObject>("EdgeGlow/Speed_Track_02");
	GameObject m_GlowSpeedStraightTile = Resources.Load<GameObject>("EdgeGlow/Speed_Track_Straight_02");
	GameObject m_GlowSpeedCornerTile = Resources.Load<GameObject>("EdgeGlow/Speed_Track_Corner_02");
	GameObject m_GlowSpikeTile = Resources.Load<GameObject>("EdgeGlow/Spike_02");
	GameObject m_GlowSwitcherTile = Resources.Load<GameObject>("EdgeGlow/Switcher_02");   
	GameObject m_GlowEndTile = Resources.Load<GameObject>("EdgeGlow/end_tile_base_03");
	#endregion //Tiles

	bool m_showGridSpaces = false;
	bool m_gridCreated = false;

	Vector2 m_scrollPosition = new Vector2(0, 0);
	
	Quaternion m_rotation = Quaternion.Euler(0, 0, 0);
	Quaternion m_deathRotation = Quaternion.Euler(-90, 0, 0);

	string m_editorMessage = "Welcome To Version 3";

	[MenuItem("Tools/Level")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(LevelEditor));
	}

	public static void CloseWindow()
	{
		EditorWindow.GetWindow(typeof(LevelEditor));
	}

	void OnGUI()
	{
		GUILayout.Label("Level Editor Version 2");
		GUILayout.Label("Level Editor Message: " + m_editorMessage);
		GUILayout.Space(10);

		GUILayout.Label("Grid Length: ", EditorStyles.boldLabel);
		m_gridLength = EditorGUILayout.IntField(m_gridLength);
		GUILayout.Label("Grid Width: ", EditorStyles.boldLabel);
		m_gridWidth = EditorGUILayout.IntField(m_gridWidth);
		
		if(m_gridLength <= 0 || m_gridLength > 200)
		{
			m_editorMessage = "Grid Length Cannot Be Below 0 or Above 200";
			m_gridLength = 1;
		}

		if(m_gridWidth <= 0 || m_gridWidth > 200)
		{
			m_editorMessage = "Grid Width Cannot Be Below 0 or Above 200";
			m_gridWidth = 1;
		}

		if((m_gridWidth > 0 || m_gridWidth <= 200) &&(m_gridLength > 0 || m_gridLength <= 200))
		{
			m_editorMessage = "Welcome To Version 3";
		}

		GUILayout.Space(10);
		if(GUILayout.Button("Set Up Grid Tiles"))
		{
			SetGrid();
			m_showGridSpaces = true;
			m_gridCreated = true;
		}

		m_showGridSpaces = GUILayout.Toggle(m_showGridSpaces, "Show Grid Tiles");

		if(m_showGridSpaces)
		{
			GUILayout.Label("Grid Tiles: Use 0 for Death Tiles, Use 1 for Outline Tiles");
			GUILayout.Label("Grid Tiles: Use 2 for Booster Tiles, Use 3 for Pusher Tiles");
			GUILayout.Label("Grid Tiles: Use 4 for Teleporter Tiles, Use 5 for Mine Tiles");
			GUILayout.Label("Grid Tiles: Use 6 for Rotator Tiles, Use 7 for Solid Tiles");
			GUILayout.Label("Grid Tiles: Use 8 for SpeedStart Tiles, Use 9 for SpeedStraight Tiles");
			GUILayout.Label("Grid Tiles: Use 10 for SpeedCorner Tiles, Use 11 for Spike Tiles");
			GUILayout.Label("Grid Tiles: Use 12 for Switcher Tiles, Use 13 for End Tile");

			GUILayout.Space(10);
			ShowGrid();

			if(m_gridCreated)
				if(GUILayout.Button("Create Grid"))
					CreateGrid();

			GUILayout.Space(50);
		}
	}

	private void SetGrid()
	{
		int count = 0;
		Debug.Log(m_gridLength + "   " + m_gridWidth);
		m_gridTiles = new List<List<int>>();
		for(int i = 0; i < m_gridLength; i++)
		{
			m_gridTiles.Add(new List<int>());
			for(int j = 0; j < m_gridWidth; j++)
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
		m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);

		GUILayout.BeginHorizontal();//BeginHorizontal BeginVertical

		for(int i = 0; i < m_gridTiles.Count; i++)
		{
			GUILayout.BeginVertical();
			for(int j = 0; j < m_gridTiles[i].Count; j++)
			{
				GUILayout.Label("x:" + i + " y:" + j);
				m_gridTiles[i][j] = EditorGUILayout.IntField(m_gridTiles[i][j]);
			}
			GUILayout.EndVertical(); //EndHorizontal
		}

		GUILayout.EndHorizontal(); //EndVertical

		GUILayout.EndScrollView();
	}
	
	private void CreateGrid()
	{
		CreateGlowGrid();
	}

	private void CreateGlowGrid()
	{
		GameObject t_go;
		for(int i = 0; i < m_gridTiles.Count; i++)
		{
			for(int j = 0; j < m_gridTiles[i].Count; j++)
			{
				if(m_gridTiles[i][j] == 0) //Death
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowDeathTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_deathRotation;
				}
				else if(m_gridTiles[i][j] == 1) //Outline
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowOutlineTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 2) //Booster
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowBoosterTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 3) //Pusher
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowPusherTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 4) //Teleporter
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowTeleporterTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 5) //Mine
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowMineTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 6) //Rotator
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowRotatorTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 7) //Solid
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSolidTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 8) //SpeedStart
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedStartTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 9) //SpeedStraight
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedStraightTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 10) //SpeedCorner
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedCornerTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 11) //Spike
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpikeTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 12) //Switcher
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSwitcherTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else if(m_gridTiles[i][j] == 13) //End
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowEndTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
				else
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowDeathTile);
					t_go.transform.position = new Vector3(i, 0, j);
					t_go.transform.rotation = m_rotation;
				}
			}
		}
	}
}
