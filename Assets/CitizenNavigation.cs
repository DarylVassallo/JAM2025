using UnityEngine;
using System.Collections.Generic;

public class CitizenNavigation : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    public GameObject destinations;
    private List<Vector3> endPoints = new List<Vector3>();  // List to store all the destination transforms
    private Transform currentPoint;
    private int currentIndex;
    public float vicinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        foreach (Transform child in destinations.transform)
        {
            endPoints.Add(child.position);  // Add each child transform to the list
        }
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = player.position;

        float distance = Vector3.Distance(transform.position, endPoints[currentIndex]);
        Debug.Log("distance: " + distance);
        Debug.Log("vicinity: " + vicinity);
        if (distance <= vicinity)
        {
            currentIndex = Random.Range(0, endPoints.Count);
        }

        agent.destination = endPoints[currentIndex];

        Debug.Log("POS: " + transform.position);
        Debug.Log("endPoints[currentIndex]: " + endPoints[currentIndex]);
    }
}
