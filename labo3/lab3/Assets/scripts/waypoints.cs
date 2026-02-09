using System;
using UnityEngine;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour
{
    public WPManager wpManager;
    public float speed = 3f;
    private int index = 0;

    public Dropdown palmNr;

    int currentIndex = 0;
    void Start()
    {
        if (wpManager.graph.pathList.Count == 0) return;

        for (int i = 0; i < wpManager.graph.pathList.Count; i++)
        {
            GameObject wp = wpManager.graph.getPathPoint(i);
            Debug.Log($"Waypoint {i}: {wp.transform.position}");
        }
    }

    void Update()
    {
        if (wpManager.graph.pathList.Count == 0) return;
        if (currentIndex >= wpManager.graph.pathList.Count) return;





        /*if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentIndex++;
        }*/

        if (palmNr.value == 0)
        {
            index = 0;
        }
        if (palmNr.value == 1)
        {
            index = 1;
        }
        if (palmNr.value == 2)
        {
            index = 2;
        }
        if (palmNr.value == 3)
        {
            index = 3;
        }
        if (palmNr.value == 4)
        {
            index = 4;
        }

        GameObject target = wpManager.graph.getPathPoint(index);
        Vector3 targetPos = target.transform.position;

        transform.position = Vector3.MoveTowards(
    transform.position,
    targetPos,
    speed * Time.deltaTime
);

    }
}
