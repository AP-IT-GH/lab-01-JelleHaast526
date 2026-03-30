using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class CubeAgentWallReward : Agent
{
    public float jumpForce = 10f;
    private Rigidbody agentRb;
    private float distToGround;

    void Start()
    {
        agentRb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position
        this.transform.localPosition = new Vector3(7, 0.5f, 0);
        this.transform.localRotation = Quaternion.Euler(0, -90f, 0);

        //reset velocity
        agentRb.linearVelocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.z);
        sensor.AddObservation(this.transform.localPosition.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get actions
        float jump = actionBuffers.DiscreteActions[0];

        //jump
        if ((jump == 1) && IsGrounded())
        {
            //Debug.Log("Jumped");
            agentRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            AddReward(-0.5f);
        }

        if (this.transform.localPosition.y < -0.5f)
        {
            AddReward(-1f);
            EndEpisodeClean();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //Debug.Log("In Heuristic");
        var actions = actionsOut.DiscreteActions;
        actions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
    //---------------------------------------------------------------------------------
    bool IsGrounded()
    {
        int groundLayer = LayerMask.GetMask("Ground"); //prevent doublejump
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Over"))
        {
            AddReward(5f);
        }
        else if (other.CompareTag("Reward"))
        {
            AddReward(5f);
        }
        else if (other.CompareTag("Top"))
        {
            AddReward(-0.5f);
            EndEpisodeClean();
        }
    }

    void EndEpisodeClean()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
            Destroy(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Reward"))
            Destroy(obj);
        EndEpisode();
    }
}