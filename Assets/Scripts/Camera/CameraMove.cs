using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ƶ�
public class CameraMove : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;

    public float Smoothing = 5f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, offset + player.transform.position, Smoothing * Time.deltaTime);
    }
}
