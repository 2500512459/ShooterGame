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
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //移动
        Move(h, v);

        //旋转
        Turning();

        //移动动画
        MoveAnimation(h, v);


    }

    private void MoveAnimation(float h, float v)
    {
        bool isMove = h != 0 || v != 0;
        if (isMove)
            anim.SetBool("IsMove", true);
        else
            anim.SetBool("IsMove", false);
    }

    private void Move(float h, float v)
    {
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movementV3);
    }
    private void Turning()
    {
        Ray CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);//获取鼠标位置

        RaycastHit groundHit;//射线检测

        int groundLayer = LayerMask.GetMask("Ground");
        if (Physics.Raycast(CameraRay, out groundHit, 100, groundLayer))
        {
            Vector3 targetV3 = groundHit.point - transform.position;// 获取射线碰撞点与玩家之间的向量
            targetV3.y = 0;
            Quaternion quaternion = Quaternion.LookRotation(targetV3);// 获取目标向量所对应的旋转
            rb.MoveRotation(quaternion);
        }
    }
}
