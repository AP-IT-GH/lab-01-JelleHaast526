using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class CubeAgentRays_old : Agent
{
    public Transform Package;
    public Transform DeliverySpace;
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;

    public override void OnEpisodeBegin()
    {

        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        this.transform.localRotation = Quaternion.identity;

        Package.localPosition = new Vector3(UnityEngine.Random.value * 8 - 4, 0.5f, UnityEngine.Random.value * 8 - 4);
        visited = new bool[25, 25]; // reset map
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Package.localPosition - transform.localPosition);
        sensor.AddObservation(DeliverySpace.localPosition - Package.localPosition);
        sensor.AddObservation(transform.forward); // new: agent facing direction
    }


    private bool[,] visited = new bool[25, 25]; // simple 10x10 grid
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        // Convert agent position to grid coordinates
        int x = Mathf.Clamp(Mathf.FloorToInt(transform.localPosition.x + 4), 0, 9);
        int z = Mathf.Clamp(Mathf.FloorToInt(transform.localPosition.z + 4), 0, 9);

        if (!visited[x, z])
        {
            Debug.Log("visited new area");
            AddReward(1f); // reward for exploring new area
            visited[x, z] = true;
        }

        AddReward(-0.01f);

        float distance = Vector3.Distance(DeliverySpace.transform.localPosition, Package.localPosition);
        AddReward(-distance * 0.01f);

        float agentToPackage = Vector3.Distance(transform.localPosition, Package.localPosition);
        AddReward(-agentToPackage * 0.01f);

        float rotation = actionBuffers.ContinuousActions[0];
        float forward = actionBuffers.ContinuousActions[1];

        // Rotatie rond Y-as
        transform.Rotate(0f, rotation * rotationMultiplier, 0f);

        // Vooruit/achteruit beweging
        transform.Translate(Vector3.forward * forward * speedMultiplier, Space.Self);

        // Reward for moving toward the package
        float previousDistance = Vector3.Distance(transform.localPosition, Package.localPosition);
        float currentDistance = Vector3.Distance(transform.localPosition + transform.forward * forward * speedMultiplier, Package.localPosition);
        AddReward(previousDistance - currentDistance); // positive reward if moving closer

        // Beloningen
        //float distanceToTarget = Vector3.Distance(this.transform.localPosition, Package.localPosition);

        float distanceToPackage = Vector3.Distance(transform.localPosition, Package.localPosition);
        if (distanceToPackage < 0.5f)
        {
            AddReward(5f); // small positive reward for reaching the package
        }

        // speler van het platform gevallen?
        if (this.transform.localPosition.y < 0)
        {
            SetReward(-10f);
            EndEpisode();
        }

        // package van het platform gevallen
        if (this.Package.localPosition.y < 0)
        {
            SetReward(-5f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Horizontal");

        continuousActionsOut[1] = Input.GetAxis("Vertical");

    }
}
