using UnityEngine;
using Unity.MLAgents;
public class DeliverySucces : MonoBehaviour
{
    public Agent Obelix;
    public CubeAgentRays ObelixVar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obelix") && ObelixVar.hasMenhir)
        {
            Obelix.AddReward(4f);
            Debug.Log("delivered");
            Obelix.EndEpisode();
        }
    }
}
