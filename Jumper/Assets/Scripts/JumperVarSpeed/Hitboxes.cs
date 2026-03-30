using Unity.MLAgents.SideChannels;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private float speedObstacle;
    private Rigidbody rb;

    void Start()
    {
        speedObstacle = Random.Range(5f, 20f);
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody on object!");
            return;
        }
    }

    void Update()
    {
        if (transform.position.x > 15f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(speedObstacle, 0, 0);
    }
}