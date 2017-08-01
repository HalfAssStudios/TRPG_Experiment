using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AstarTestUnit : MonoBehaviour
{
    public AStar m_aStar;
	public float m_moveSpeed;

    private Vector3 m_rotation;
	public float m_rotationSpeed;
	
    public Node m_startNode;
    public Node m_goalNode;
    public Node m_targetNode;

    public int maxMoveDistance = 2;

    public int[,] myArray = {
        { 0, 0, 1, 0, 0 },
        { 0, 1, 1, 1, 0 },
        { 1, 1, 1, 1, 1 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 1, 0, 0 }
    };



    void Start() 
	{

        //m_aStar.CalculatePath(m_startNode, m_goalNode);
        //m_aStar.SetPathToGoal(m_goalNode);

        //m_targetNode = m_aStar.FindNearestNode(m_position);
    }
    public List<Node> list = new List<Node>();

    void CheckLeftClick()
    {
        Node nearestNode = m_aStar.FindNearestNode(transform.position);

        //nearestNode.gameObject.GetComponent<Renderer>().material.color = Color.blue;


        list = m_aStar.ColorMovementPositions(2, nearestNode);

    }

    void CheckClick()
    {
        RaycastHit t_rayHit;
        Ray t_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(t_ray, out t_rayHit, 1000f))
        {
            if (t_rayHit.transform.gameObject.tag == "Ground")
            {
                m_aStar.Reset();

                m_startNode = m_aStar.FindNearestNode(transform.position);
                m_goalNode = t_rayHit.transform.gameObject.GetComponent<Node>();

                m_aStar.CalculatePath(m_startNode, m_goalNode);
                m_aStar.SetPathToGoal(m_goalNode);

                m_targetNode = m_startNode;
            }
        }

    }
	void Update() 
	{
        if (!m_aStar.isInitialized)
            return;

        if (Input.GetMouseButtonDown(0))
            CheckLeftClick();

        if (Input.GetMouseButtonDown(1))
            CheckClick();

        Node t_temp = m_aStar.CheckPath(m_targetNode);
        if (t_temp != null)
            m_targetNode = t_temp;

        MoveTowards(m_targetNode);
	}

	public void MoveTowards(Node i_targetNode)
	{
        if (i_targetNode == null)
            return;

        Vector3 t_direction = i_targetNode.transform.position - transform.position;
        t_direction.Normalize();
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(t_direction), m_rotationSpeed * Time.deltaTime);
		
		Vector3 t_forward = transform.TransformDirection(Vector3.forward);
        transform.position += t_forward * m_moveSpeed * Time.deltaTime;
		transform.position = transform.position;
	}
}
