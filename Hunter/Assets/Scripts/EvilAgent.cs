using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SocialPlatforms;

public class EvilAgent : Agent
{
    public float speedMultiplier = 3f;
    public float rotationMultiplier = 0.5f;

    Rigidbody rb;
    void Start() { rb = GetComponent<Rigidbody>(); }

    public override void OnEpisodeBegin()
    {
        Vector3 evilAgentSpawn;
        evilAgentSpawn = GetSpawn();
        // Reset agent position
        this.transform.localPosition = evilAgentSpawn;
        this.transform.localRotation = Quaternion.identity;
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
            AddReward(-3f);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Grabber"))
        {
            Debug.Log("caught grabber");
            AddReward(6f);
            EndEpisode();
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            AddReward(-1f);
        }
    }
}

