using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Player movement speed
    /// </summary>
    [Header("Move Speed")]
    public float speed = 5f;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movementV3);
    }
}
