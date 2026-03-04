using UnityEngine;
using Unity.MLAgents;
public class DeliverySucces : MonoBehaviour
{
    public Agent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Package"))
        {
            agent.AddReward(4f);
            agent.EndEpisode();
        }
    }
}
