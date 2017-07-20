using UnityEngine;
using UnityEditor;
using System.Collections;

public class TileEditorWindow : EditorWindow
{
	enum TileTypes{DEATH, OUTLINE, BOOSTER, PUSHER, TELEPORTER, MINE, ROTATOR, SOLID, SPEED, SPEEDST, SPEEDC, SPIKE, SWITCHER, END};

	Vector2 m_scrollPosition = new Vector2(0, 0);

	static GameObject m_selectedObject;
	static GameObject[] m_selectedObjects;

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
	
	bool m_GlowTheme = true;

	[MenuItem("Tools/Tile")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(TileEditorWindow));
	}
	
	public static void CloseWindow()
	{
		EditorWindow.GetWindow(typeof(TileEditorWindow));
	}

	void OnGUI()
	{
		GUILayout.Label("Tile Themes");
		GUILayout.BeginHorizontal();
		
		GUILayout.EndHorizontal();
		
		m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);

		if(m_selectedObjects.Length > 0)
		{
			GUILayout.Label("MultiSelection");
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Death"))
				SwitchTilesToGlow(TileTypes.DEATH);
			if(GUILayout.Button("Outline"))
				SwitchTilesToGlow(TileTypes.OUTLINE);
			if(GUILayout.Button("Booster"))
				SwitchTilesToGlow(TileTypes.BOOSTER);
			if(GUILayout.Button("Pusher"))
				SwitchTilesToGlow(TileTypes.PUSHER);
			if(GUILayout.Button("Teleporter"))
				SwitchTilesToGlow(TileTypes.TELEPORTER);
			if(GUILayout.Button("Mine"))
				SwitchTilesToGlow(TileTypes.MINE);
			if(GUILayout.Button("Rotator"))
				SwitchTilesToGlow(TileTypes.ROTATOR);
			if(GUILayout.Button("Solid"))
				SwitchTilesToGlow(TileTypes.SOLID);
			if(GUILayout.Button("Speed"))
				SwitchTilesToGlow(TileTypes.SPEED);
			if(GUILayout.Button("Speed Straight"))
				SwitchTilesToGlow(TileTypes.SPEEDST);
			if(GUILayout.Button("Speed Corner"))
				SwitchTilesToGlow(TileTypes.SPEEDC);
			if(GUILayout.Button("Spike"))
				SwitchTilesToGlow(TileTypes.SPIKE);
			if(GUILayout.Button("Switcher"))
				SwitchTilesToGlow(TileTypes.SWITCHER);
			if(GUILayout.Button("End"))
				SwitchTilesToGlow(TileTypes.END);
			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.Label("Swapper: Please Select A Tile or A Group of Tiles");
		}

		GUILayout.Space(10);

		if(m_selectedObjects.Length > 0)
		{
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Rotate 90 CW"))
				RotateClockWise(90);
			if(GUILayout.Button("Rotate 90 CCW"))
				RotateCounterClockWise(-90);
			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.Label("Rotations: Please Select A Tile or A Group of Tiles");
		}
		GUILayout.EndScrollView();
	}

	void RotateClockWise(float i_degrees)
	{
		foreach(GameObject t_go in m_selectedObjects)
		{
			t_go.transform.Rotate(0, i_degrees, 0);
		}
	}

	void RotateCounterClockWise(float i_degrees)
	{
		foreach(GameObject t_go in m_selectedObjects)
		{
			t_go.transform.Rotate(0, i_degrees, 0);
		}
	}

	void SwitchTilesToGlow(TileTypes i_type)
	{
		GameObject t_go;
		Transform t_transform;
		
		foreach(GameObject t_obj in m_selectedObjects)
		{
			t_transform = t_obj.transform;

			//if(t_obj.GetComponent<DeathTile>() != null)
			//{
			//	t_transform.rotation = Quaternion.identity;
			//}
			
			switch(i_type)
			{
				case TileTypes.DEATH:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowDeathTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
					break;
				}
				case TileTypes.OUTLINE:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowOutlineTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.BOOSTER:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowBoosterTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.PUSHER:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowPusherTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.TELEPORTER:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowTeleporterTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.MINE:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowMineTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.ROTATOR:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowRotatorTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SOLID:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSolidTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SPEED:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedStartTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SPEEDST:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedStraightTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SPEEDC:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpeedCornerTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SPIKE:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSpikeTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.SWITCHER:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowSwitcherTile);
					t_go.transform.position = t_transform.position;
					t_go.transform.rotation = t_transform.rotation;
					break;
				}
				case TileTypes.END:
				{
					t_go = (GameObject)PrefabUtility.InstantiatePrefab(m_GlowEndTile);
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
