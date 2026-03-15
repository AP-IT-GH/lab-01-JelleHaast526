using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SocialPlatforms;

public class CubeAgentRays : Agent
{
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;
    public bool hasMenhir;
    public Transform menhir;
    public Transform deliveryZone;
    private float previousDistance;

    private float lastDistToZone;
    private float lastDistToMenhir;

    public override void OnEpisodeBegin()
    {
        // Reset agent position
        this.transform.localPosition = new Vector3(0, 1f, 0);
        this.transform.localRotation = Quaternion.identity;

        // Randomize package position
        foreach (GameObject m in GameObject.FindGameObjectsWithTag("Menhir"))
            Destroy(m);

        foreach (GameObject d in GameObject.FindGameObjectsWithTag("DeliveryZone"))
            Destroy(d);

        // Spawn fresh ones
        int menhirCount = 5; // or randomize this
        for (int i = 0; i < menhirCount; i++)
            SpawnMenhir();

        int deliveryZones = 1;
        for (int i = 0; i < deliveryZones; i++)
            Spawndeliveryzone();

        hasMenhir = false;
        GameObject[] menhirs = GameObject.FindGameObjectsWithTag("Menhir");
        if (menhirs.Length > 0)
            lastDistToMenhir = Vector3.Distance(transform.position, menhirs[0].transform.position);

        GameObject[] zones = GameObject.FindGameObjectsWithTag("DeliveryZone");
        if (zones.Length > 0)
            lastDistToZone = Vector3.Distance(transform.position, zones[0].transform.position);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.forward);
        sensor.AddObservation(hasMenhir);

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Base penalty for taking time
        AddReward(-0.001f);
        if (GameObject.FindGameObjectsWithTag("Menhir").Length == 0 && !hasMenhir)
        {
            AddReward(6f);
            EndEpisode();
            return;
        }

        /*if (hasMenhir)
        {
            GameObject[] zones = GameObject.FindGameObjectsWithTag("DeliveryZone");
            if (zones.Length > 0)
            {
                GameObject nearestZone = zones[0]; // FindGameObjectsWithTag returns an array, not a single GameObject
                float dist = Vector3.Distance(transform.position, nearestZone.transform.position);
                AddReward((lastDistToZone - dist) * 0.01f);
                lastDistToZone = dist;
            }
        }

        if (!hasMenhir)
        {
            GameObject[] menhirs = GameObject.FindGameObjectsWithTag("Menhir");
            if (menhirs.Length > 0)
            {
                float dist = Vector3.Distance(transform.position, menhirs[0].transform.position);
                AddReward((lastDistToMenhir - dist) * 0.01f);
                lastDistToMenhir = dist;
            }
        }*/


        // Get actions
        float rotation = actionBuffers.ContinuousActions[0];
        float forward = actionBuffers.ContinuousActions[1];

        // Apply movement
        transform.Rotate(0f, rotation * rotationMultiplier, 0f);
        transform.Translate(Vector3.forward * forward * speedMultiplier, Space.Self);

        // Check fall conditions
        if (this.transform.localPosition.y < 0)
        {
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
        //Debug.Log($"Heuristic called: H={continuousActionsOut[0]}, V={continuousActionsOut[1]}");
    }

    //redndering play area
    private void SpawnMenhir()
    {
        Vector3 spawnPos = new Vector3(
            UnityEngine.Random.value * 14 - 7,
            0.5f,
            UnityEngine.Random.value * 14 - 7
        );

        Instantiate(menhir, spawnPos, Quaternion.identity);
    }

    private void Spawndeliveryzone()
    {
        Vector3 spawnPos = new Vector3(
            UnityEngine.Random.value * 14 - 7,
            0.5f,
            UnityEngine.Random.value * 14 - 7
        );

        Instantiate(deliveryZone, spawnPos, Quaternion.identity);
    }
    //---------------------------------------------------------------------

    //trigger boxes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeliveryZone") && hasMenhir)
        {
            AddReward(3f);
            hasMenhir = false;
            // Re-initialize so next menhir approach starts clean
            /*GameObject[] menhirs = GameObject.FindGameObjectsWithTag("Menhir");
            if (menhirs.Length > 0)
                lastDistToMenhir = Vector3.Distance(transform.position, menhirs[0].transform.position);
            Debug.Log("delivered");*/
        }
        /*else if (other.CompareTag("DeliveryZone") && !hasMenhir)
        {
            AddReward(-0.01f);
        }*/
        else if (other.CompareTag("Menhir") && !hasMenhir)
        {
            AddReward(1f);
            Destroy(other.gameObject);
            hasMenhir = true;
            /*Debug.Log("picked up menhir");
            GameObject[] zones = GameObject.FindGameObjectsWithTag("DeliveryZone");
            if (zones.Length > 0)
                lastDistToZone = Vector3.Distance(transform.position, zones[0].transform.position);*/
        }
        else if (other.CompareTag("Menhir") && hasMenhir)
        {
            AddReward(-0.01f);
            Debug.Log("touched menhir when already carrying one");
        }
    }
}

