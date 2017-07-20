using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour
{
    private int listIterator = 1;

    private Node startNode;
    private Node goalNode;
    private Node nextNode;

    public List<Node> closedList = new List<Node>();
    public List<Node> openList = new List<Node>();
    public List<Node> pathToGoal = new List<Node>();
    public List<Node> allNodes = new List<Node>();

    public List<List<Node>> nodeGrid = new List<List<Node>>();

    public int[,] nodeArray = {

        { 0, 0, 1, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 1, 1, 1, 1, 1 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 0, 0 }
    };

    public GameObject nodePrefab;

    void Awake()
    {
        for (int i = 0; i < 11; i++) //row
        {
            nodeGrid.Add(new List<Node>());
            for (int j = 0; j < 11; j++) //column
            {
                GameObject nodeObj = GameObject.Instantiate(nodePrefab, new Vector3(i * 5, 0, j * 5), Quaternion.identity);

                Node node = nodeObj.GetComponent<Node>();

                node.movementCost = Random.Range(1,2);
                node.SetColorBasedOnCost();

                node.gridPosition = new Vector2(i, j);

                allNodes.Add(node);
                nodeGrid[i].Add(node);
            }
        }

        nodeGrid[5][5].movementCost = 3;

        for (int i = 0; i < allNodes.Count; i++)
        {
            allNodes[i].InitializeNeighbors();
        }

        //Node[] t_nodes = GameObject.FindObjectsOfType<Node>();

        //for (int i = 0; i < t_nodes.Length; i++)
        //{
        //    allNodes.Add(t_nodes[i]);
        //}
    }

    void SetStartAndGoal(Node start, Node goal)
    {
        Reset();

        startNode = start;
        goalNode = goal;

        startNode.G = 0;
        startNode.H = Vector3.Distance(startNode.transform.position, goalNode.transform.position);
        startNode.F = startNode.H + startNode.G;
        startNode.Parent = null;

        openList.Add(startNode);
    }
    public List<Node> ColorMovementPositions(int maxMovementDistance, Node node)
    {
        List<Node> nodeList = new List<Node>();

        int gridPositionX = (int)node.gridPosition.x;
        int gridPositionY = (int)node.gridPosition.y;

        nodeGrid[gridPositionX][gridPositionY].costText.color = Color.red;



        for (int i = gridPositionX - maxMovementDistance; i < gridPositionX + (maxMovementDistance + 1); i++) //row
        {
            if (i >= 0 && i < 11)
            {
                for (int j = gridPositionY - maxMovementDistance; j < gridPositionY + (maxMovementDistance + 1); j++) //column
                {
                    if (j >= 0 && j < 11)
                    {
                        CalculatePath(node, nodeGrid[i][j]);
                        SetPathToGoal(nodeGrid[i][j]);

                        float totalCost = 0;
                        foreach (Node n in pathToGoal)
                        {
                            //n.costText.color = Color.green;
                            totalCost += n.movementCost;
                        }

                        totalCost -= node.movementCost;
                        nodeGrid[i][j].costText.text = totalCost.ToString();

                        if (totalCost > maxMovementDistance)
                        {
                            nodeGrid[i][j].costText.color = Color.black;
                        }
                        else
                        {
                            nodeList.Add(nodeGrid[i][j]);
                            nodeGrid[i][j].costText.color = Color.green;
                        }
                    }

                }
            }
        }

        return nodeList;


        //node.neighborList[(int)Node_Neighbors.East].gameObject.GetComponent<Renderer>().material.color = Color.blue;
        //node.neighborList[(int)Node_Neighbors.West].gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public Node FindNearestNode(Vector3 targetPosition)
    {
        int index = 0;
        float bestDistance = Mathf.Infinity;

        for (int i = 0; i < allNodes.Count; i++)
        {
            float distance = Vector3.Distance(allNodes[i].transform.position, targetPosition);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                index = i;
            }
        }

        return allNodes[index];
    }

    public void CalculatePath(Node i_start, Node i_goal)
    {
        SetStartAndGoal(i_start, i_goal);

        while (!(openList.Count == 0))
        {
            nextNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].F < nextNode.F || (openList[i].F == nextNode.F && openList[i].H < nextNode.H))
                {
                    nextNode = openList[i];
                }
            }

            openList.Remove(nextNode);
            closedList.Add(nextNode);

            if (nextNode == goalNode)
            {
                return;
            }

            for (int i = 0; i < (int)Node_Neighbors.Total; i++)
            {
                if (ClosedContains(nextNode.ReturnNeighborNode(i)) || nextNode.ReturnNeighborNode(i) == null)
                {
                    continue;
                }

                float cost = nextNode.G + nextNode.ReturnNeighborNode(i).movementCost;

                if (cost < nextNode.ReturnNeighborNode(i).G || !OpenContains(nextNode.ReturnNeighborNode(i)))
                {
                    nextNode.ReturnNeighborNode(i).G = (cost);
                   // nextNode.ReturnNeighborNode(i).H = (Vector3.Distance(nextNode.ReturnNeighborNode(i).transform.position, goalNode.transform.position));
                    nextNode.ReturnNeighborNode(i).Parent = (nextNode);

                    if (!OpenContains(nextNode.ReturnNeighborNode(i)))
                    {
                        openList.Add(nextNode.ReturnNeighborNode(i));
                    }
                }
            }
        }
    }

    public void SetPathToGoal(Node parent)
    {
        if (parent == null)
            return;

        //parent.costText.color = Color.red;

        if (parent == startNode)
        {
            pathToGoal.Add(startNode);

            return;
        }

        pathToGoal.Add(parent);

        SetPathToGoal(parent.Parent);
    }

    public Node CheckPath(Node targetNode)
    {
        if (targetNode == null)
            return null;

        if (Vector3.Distance(targetNode.transform.position, transform.position) < 1.5f)
        {
            if (targetNode != goalNode)
            {
                int index = pathToGoal.Count - ++listIterator;

                if (index >= 0)
                {
                    return pathToGoal[index];
                }
            }
            else
            {
                Reset();
                return null;
            }
        }

        return pathToGoal[pathToGoal.Count - listIterator];
    }

    private bool OpenContains(Node i_node)
    {
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i] == i_node)
                return true;
        }
        return false;
    }

    private bool ClosedContains(Node i_node)
    {
        for (int i = 0; i < closedList.Count; i++)
        {
            if (closedList[i] == i_node)
                return true;
        }
        return false;
    }

    public void Reset()
    {
        //for (int i = 0; i < pathToGoal.Count; i++)
        //{
        //    pathToGoal[i].GetComponent<Renderer>().material.color = Color.white;
        //}

        listIterator = 1;

        openList.Clear();
        closedList.Clear();
        pathToGoal.Clear();
    }
}