using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class CubeAgent2Side : Agent
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
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        this.transform.localRotation = Quaternion.Euler(0, -90f, 0);

        //Reset velocity
        agentRb.linearVelocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
            Destroy(obj);

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
            AddReward(-0.5f); // -0.5 -> -0.2
        }

        if (this.transform.localPosition.y < -0.5f)
        {
            AddReward(-1f);
            EndEpisode();
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
        int groundLayer = LayerMask.GetMask("Ground");
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Over"))
        {
            AddReward(5f);
        }
        else if (other.CompareTag("Top"))
        {
            AddReward(-0.5f); //-0.5 -> -1.5f
            EndEpisode();
        }
    }
}