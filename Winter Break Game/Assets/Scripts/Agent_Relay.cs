using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent_Relay : MonoBehaviour
{
    [SerializeField] private GameObject pointHolder;
    [SerializeField] private GameObject childTextObject;
    public List<GameObject> movePoints = new List<GameObject>();
    private NavMeshAgent agent;
    private int currentPoint = 0;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (Transform child in pointHolder.transform)
        {
            movePoints.Add(child.gameObject);
        }

        agent.SetDestination(movePoints[currentPoint].transform.position);
    }

    void FixedUpdate()
    {
        CheckDistanceToDistance();
        ChildTextLookAtCamera();
    }

    private void ChildTextLookAtCamera()
    {
        childTextObject.transform.LookAt(Camera.main.transform);
    }

    private void CheckDistanceToDistance()
    {
        if (agent.remainingDistance < 0.5f)
        {
            currentPoint++;
            if (currentPoint >= movePoints.Count)
            {
                currentPoint = 0;
            }
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        agent.SetDestination(movePoints[currentPoint].transform.position);
    }
}
