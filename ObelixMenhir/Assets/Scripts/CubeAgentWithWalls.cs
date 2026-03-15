using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SocialPlatforms;

public class CubeAgentRaysWalls : Agent
{
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;
    public bool hasMenhir;
    public Transform menhir;
    public Transform deliveryZone;
    private float previousDistance;

    private float lastDistToZone;
    private float lastDistToMenhir;
    Rigidbody rb;
    void Start() { rb = GetComponent<Rigidbody>(); }

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
        sensor.AddObservation(hasMenhir ? 1f : -1f);

        // Delivery zone in local space
        GameObject[] zones = GameObject.FindGameObjectsWithTag("DeliveryZone");
        if (zones.Length > 0)
        {
            Vector3 toZone = zones[0].transform.position - transform.position;
            Vector3 localZone = transform.InverseTransformDirection(toZone);
            float dist = Mathf.Max(toZone.magnitude, 0.5f);
            sensor.AddObservation(localZone / dist);
            sensor.AddObservation(dist);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
            sensor.AddObservation(0f);
        }

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Base penalty for taking time
        AddReward(-0.005f);
        if (GameObject.FindGameObjectsWithTag("Menhir").Length == 0 && !hasMenhir)
        {
            AddReward(6f);
            EndEpisode();
            Debug.Log("got all menhirs");
            return;
        }


        // Get actions
        float rotation = actionBuffers.ContinuousActions[0];
        float forward = actionBuffers.ContinuousActions[1];

        // Apply movement
        transform.Rotate(0f, rotation * rotationMultiplier, 0f);
        rb.MovePosition(rb.position + transform.forward * forward * speedMultiplier * Time.deltaTime); // new method for moving

        // Check fall conditions
        /*if (this.transform.localPosition.y < 0)
        {
            AddReward(-1f);
            EndEpisode();
        }*/
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
        Vector3 spawnPos;

        // 40% chance of edge case spawn
        if (Random.value < 0.2f)
        {
            spawnPos = GetEdgeCaseSpawn();
        }
        else
        {
            spawnPos = GetNormalSpawn();
        }

        Instantiate(menhir, spawnPos, Quaternion.identity);
    }

    private Vector3 GetNormalSpawn()
    {
        return new Vector3(
            Random.Range(-5f, 5f),
            0.5f,
            Random.Range(-5f, 5f)
        );
    }

    private Vector3 GetEdgeCaseSpawn()
    {
        // Pick one of four wall edges randomly
        int wall = Random.Range(0, 4);
        switch (wall)
        {
            case 0: return new Vector3(Random.Range(-7f, 7f), 0.5f, 6f);  // top wall
            case 1: return new Vector3(Random.Range(-7f, 7f), 0.5f, -6f); // bottom wall
            case 2: return new Vector3(6f, 0.5f, Random.Range(-7f, 7f));  // right wall
            default: return new Vector3(-6f, 0.5f, Random.Range(-7f, 7f));// left wall
        }
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
            AddReward(4f);
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
            AddReward(2f);
            Destroy(other.gameObject);
            hasMenhir = true;
            /*Debug.Log("picked up menhir");
            GameObject[] zones = GameObject.FindGameObjectsWithTag("DeliveryZone");
            if (zones.Length > 0)
                lastDistToZone = Vector3.Distance(transform.position, zones[0].transform.position);*/
        }
        else if (other.CompareTag("Menhir") && hasMenhir)
        {
            AddReward(-4f);
            Debug.Log("touched menhir when already carrying one");
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            AddReward(-0.001f);
        }
    }
}

