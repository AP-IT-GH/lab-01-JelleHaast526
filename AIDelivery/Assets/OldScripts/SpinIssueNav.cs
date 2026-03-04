using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class SpinIssue : Agent
{
    public Transform Package;
    public Transform DeliverySpace;
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;

    private float previousDistanceToPackage;
    private bool hasReachedPackage;
    private bool[,] visited = new bool[25, 25];

    public override void OnEpisodeBegin()
    {
        // Reset agent position
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        this.transform.localRotation = Quaternion.identity;

        // Randomize package position
        Package.localPosition = new Vector3(
            UnityEngine.Random.value * 8 - 4,
            0.5f,
            UnityEngine.Random.value * 8 - 4
        );

        // Reset tracking variables
        visited = new bool[25, 25];
        previousDistanceToPackage = Vector3.Distance(transform.localPosition, Package.localPosition);
        hasReachedPackage = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Package.localPosition - transform.localPosition);
        sensor.AddObservation(DeliverySpace.localPosition - Package.localPosition);
        sensor.AddObservation(transform.forward);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Base penalty for taking time
        AddReward(-0.001f);

        // Get actions
        float rotation = actionBuffers.ContinuousActions[0];
        float forward = actionBuffers.ContinuousActions[1];

        // Apply movement
        transform.Rotate(0f, rotation * rotationMultiplier, 0f);
        transform.Translate(Vector3.forward * forward * speedMultiplier, Space.Self);

        // Calculate distances
        float currentDistanceToPackage = Vector3.Distance(transform.localPosition, Package.localPosition);

        // Exploration reward
        int x = Mathf.Clamp(Mathf.FloorToInt(transform.localPosition.x + 4), 0, 24);
        int z = Mathf.Clamp(Mathf.FloorToInt(transform.localPosition.z + 4), 0, 24);

        /*if (!visited[x, z])
        {
            AddReward(0.05f);
            visited[x, z] = true;
        }*/

        // Reward for moving toward package
        float distanceImprovement = previousDistanceToPackage - currentDistanceToPackage;
        AddReward(distanceImprovement * 0.5f);

        previousDistanceToPackage = currentDistanceToPackage;

        // Package pickup reward
        if (!hasReachedPackage && currentDistanceToPackage < 0.5f)
        {
            AddReward(10f); // Main reward for reaching package
            hasReachedPackage = true;
        }

        // Check fall conditions
        if (this.transform.localPosition.y < 0)
        {
            AddReward(-10f);
            EndEpisode();
        }
        else if (this.Package.localPosition.y < 0)
        {
            AddReward(-5f);
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