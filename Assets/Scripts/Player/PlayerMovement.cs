using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Player movement speed
    /// </summary>
    [Header("Move Speed")]
    public float speed = 5f;

    private Rigidbody rb;
    private Animator anim;
    private float h;
    private float v;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 获取输入
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 更新动画状态
        MoveAnimation(h, v);
    }

    private void FixedUpdate()
    {
        // 移动
        Move(h, v);

        // 旋转
        Turning();
    }

    private void MoveAnimation(float h, float v)
    {
        bool isMove = h != 0 || v != 0;
        anim.SetBool("IsMove", isMove);
    }

    private void Move(float h, float v)
    {
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movementV3);
    }

    private void Turning()
    {
        Ray CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit groundHit;
        int groundLayer = LayerMask.GetMask("Ground");
        
        if (Physics.Raycast(CameraRay, out groundHit, 100, groundLayer))
        {
            Vector3 targetV3 = groundHit.point - transform.position;
            targetV3.y = 0;
            Quaternion quaternion = Quaternion.LookRotation(targetV3);
            rb.MoveRotation(quaternion);
        }
    }
}
