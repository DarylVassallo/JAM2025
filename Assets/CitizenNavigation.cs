using UnityEngine;
using System.Collections.Generic;

public class CitizenNavigation : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;

    public GameObject destinations;
    private List<Vector3> endPoints = new List<Vector3>();  // List to store all the destination transforms
    private Transform currentPoint;
    private int currentIndex;
    public float vicinity;

    public bool isSick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        foreach (Transform child in destinations.transform)
        {
            endPoints.Add(child.position);  // Add each child transform to the list
        }
        currentIndex = Random.Range(0, endPoints.Count);

        isSick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSick)
        {
            MeshRenderer sickIcon = transform.GetChild(0).GetComponent<MeshRenderer>();
            sickIcon.enabled = true;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, endPoints[currentIndex]);
            if (distance <= vicinity)
            {
                currentIndex = Random.Range(0, endPoints.Count);
            }

            agent.destination = endPoints[currentIndex];
        }
    }
}
