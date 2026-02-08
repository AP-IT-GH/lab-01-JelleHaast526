using UnityEngine;

public class FollowWaypointNonAI : MonoBehaviour
{
    public Transform[] waypoints;

    //can be boudn to tank speed or visa versa to keep tank on exact cube track
    public float moveSpeed = 5f;
    private int currentWaypoint = 0;

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);


        //waypoint check
        if (Vector3.Distance(transform.position, target.position) < 2f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }
    }
}
