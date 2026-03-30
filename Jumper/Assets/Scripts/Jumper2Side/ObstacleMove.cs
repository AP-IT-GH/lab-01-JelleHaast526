using UnityEngine;

public class Obstacle2Side : MonoBehaviour
{
    private float speedObstacle;
    private Rigidbody rb;
    private float direction;

    void Start()
    {
        speedObstacle = Random.Range(8f, 20f);
        rb = GetComponent<Rigidbody>();

        // if rotated 180 it comes from the right, so it moves left
        direction = (transform.rotation.eulerAngles.y > 90f) ? -1f : 1f;
        Debug.Log(direction);
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 15f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(speedObstacle * direction, 0, 0);
    }
}