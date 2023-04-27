using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flocking_Wait : MonoBehaviour
{
    public NavMeshAgent spawnPosition;
    public Vector3 targetPosition;

    [HideInInspector]
    public bool pathAvailable;
    public NavMeshPath navMeshPath;
    bool atposition;

    void Start()
    {
        spawnPosition = this.gameObject.GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
    }

    void Update()
    {
        if (Vector3.Distance(spawnPosition.destination ,targetPosition) < 10)
        {
            if (CalculateNewPath() == true)
            {
                pathAvailable = true;
                print("Path available");
            }
            else
            {
                pathAvailable = false;
                print("Path not available");
            }
        }
    }

    bool CalculateNewPath()
    {
        spawnPosition.CalculatePath(targetPosition, navMeshPath);
        print("New path calculated");
        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
