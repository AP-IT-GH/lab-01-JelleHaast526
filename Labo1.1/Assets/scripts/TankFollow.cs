using UnityEngine;

public class TankFollow : MonoBehaviour
{
    public Transform targetPos;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Transform target = targetPos;

        Vector3 direction = (target.position - rb.position).normalized;
        Vector3 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        //rotate next target
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(
                Quaternion.Slerp(rb.rotation, lookRotation, rotateSpeed * Time.fixedDeltaTime)
            );
        }

    }
}
