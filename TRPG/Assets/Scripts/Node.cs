using UnityEngine;
using System.Collections.Generic;

public enum Node_Neighbors
{
    North,
    //NorthEast,
    East,
    //SouthEast,
    South,
    //SouthWest,
    West,
    //NorthWest,
    Total
}

public class Node : MonoBehaviour
{
    public GameObject positionalObject;

    private Node parentNode = null;

    public List<Node> neighborList = new List<Node>();

    private float f = 0f;
    private float g = 0f;
    private float h = 0f;

    public Node Parent { get { return parentNode; } set { parentNode = value; } }
    public float F { get { return f; } set { f = value; } }
    public float G { get { return g; } set { g = value; } }
    public float H { get { return h; } set { h = value; } }

    public Vector2 gridPosition;

    public int movementCost = 1;

    public TextMesh costText;

    void Awake()
    {
        positionalObject = gameObject;

        //InitializeNeighbors();
    }

    public void SetColorBasedOnCost()
    {
        Renderer renderer = GetComponent<Renderer>();
        switch (movementCost)
        {
            case 1:
                renderer.material.color = Color.white;
                break;
            case 2:
                renderer.material.color = Color.blue;
                break;
            case 3:
                renderer.material.color = Color.red + Color.yellow;
                break;
            default:
                renderer.material.color = Color.black;
                break;
        }
    }

    public void InitializeNeighbors()
    {
        Vector3 direction = transform.forward;

        for (int i = 0; i < (int)Node_Neighbors.Total; i++)
        {
            neighborList.Add(null);

            Debug.DrawRay(transform.position, direction * 15, Color.red, 10f);

            PerformRayCast(transform.position, direction, 15, i);

            direction = Quaternion.Euler(0f, 90f, 0f) * direction;
        }
    }

    public void PerformRayCast(Vector3 i_position, Vector3 i_direction, int i_distance, int i_index)
    {
        RaycastHit t_rayHit;
        if (Physics.Raycast(i_position, i_direction, out t_rayHit, i_distance))
        {
            if (t_rayHit.transform.tag == "Ground")
            {
                neighborList[i_index] = t_rayHit.transform.gameObject.GetComponent<Node>();
            }
        }
        else
        {
            neighborList[i_index] = null;
        }
    }

    public void SetSuccessors(Node north, Node south, Node east, Node west)
    {
        neighborList[(int)Node_Neighbors.North] = north;
        neighborList[(int)Node_Neighbors.South] = south;
        neighborList[(int)Node_Neighbors.East] = east;
        neighborList[(int)Node_Neighbors.West] = west;
    }

    public Node ReturnNeighborNode(int index)
    {
        return neighborList[index];
    }
}