using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SocialPlatforms;

public class GrabberAgent : Agent
{
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;
    public Transform objective;

    Rigidbody rb;
    void Start() { rb = GetComponent<Rigidbody>(); }

    public override void OnEpisodeBegin()
    {
        Vector3 grabberAgent;
        grabberAgent = GetSpawn();
        // Reset agent position
        this.transform.localPosition = grabberAgent;
        this.transform.localRotation = Quaternion.identity;

        // Randomize package position
        foreach (GameObject m in GameObject.FindGameObjectsWithTag("Obj"))
            Destroy(m);


        // Spawn fresh ones
        int objectiveCount = 5; // or randomize this
        for (int i = 0; i < objectiveCount; i++)
            SpawnObj();
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Base penalty for taking time
        AddReward(-0.0005f);
        if (GameObject.FindGameObjectsWithTag("Obj").Length == 0)
        {
            AddReward(10f);
            EndEpisode();
            Debug.Log("got all Obj");
            return;
        }


        // Get actions
        float rotation = actionBuffers.ContinuousActions[0];
        float forward = actionBuffers.ContinuousActions[1];

        // Apply movement
        transform.Rotate(0f, rotation * rotationMultiplier, 0f);
        rb.MovePosition(rb.position + transform.forward * forward * speedMultiplier * Time.deltaTime); // new method for moving

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
        //Debug.Log($"Heuristic called: H={continuousActionsOut[0]}, V={continuousActionsOut[1]}");
    }

    //redndering play area
    private void SpawnObj()
    {
        Vector3 spawnPos;

        spawnPos = GetSpawn();

        Instantiate(objective, spawnPos, Quaternion.identity);
    }

    private Vector3 GetSpawn()
    {
        return new Vector3(
            Random.Range(-7f, 7f),
            0.5f,
            Random.Range(-7f, 7f)
        );
    }
    //---------------------------------------------------------------------

    //trigger boxes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obj"))
        {
            AddReward(2f);
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Hunter"))
        {
            Debug.Log("caught");
            AddReward(-3f);
            EndEpisode();
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            AddReward(-1f);
        }
    }
}

