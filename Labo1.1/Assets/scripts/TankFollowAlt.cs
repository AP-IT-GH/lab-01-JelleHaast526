using UnityEngine;

public class TankFollowAlt : MonoBehaviour
{
    public Transform targetPos;
    private float moveSpeed = 100f;
    public float rotateSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (targetPos == null) return;

        Vector3 direction = targetPos.position - rb.position;
        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            //movetank according on the distance of the cube (rubberbanding)
            //this way whe cube far => movetank at its max speed of 100f in our case / if close by cube => move small distance calc step
            //can also skip the max move speed and use the magnitude value for rubber banding value (can cause weird behaviour at high speeds) 
            float step = Mathf.Min(moveSpeed * Time.fixedDeltaTime, distance);
            Vector3 newPosition = rb.position + direction.normalized * step;
            rb.MovePosition(newPosition);

            //rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, rotateSpeed * Time.fixedDeltaTime));
        }
    }

}
