using UnityEngine;
using Unity.MLAgents;
public class HitBoxOver : MonoBehaviour
{
    public Agent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.AddReward(5f);
            agent.EndEpisode();
        }
    }
}
