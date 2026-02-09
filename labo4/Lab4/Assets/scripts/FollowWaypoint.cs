using UnityEngine;
using UnityEngine.AI;

public class FollowWaypointAIver : MonoBehaviour
{
    public Transform[] waypoints;
    public NavMeshAgent agent;

    private int currentWaypoint = 0;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}
