using UnityEngine;
using Unity.MLAgents;
public class PickUpMenhir : MonoBehaviour
{
    public Agent Obelix;
    public CubeAgentRays ObelixVar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obelix") && !ObelixVar.hasMenhir)
        {
            Obelix.AddReward(2f);
            Debug.Log("obelix picked up menhir");
            ObelixVar.hasMenhir = true;
        }
    }
}
