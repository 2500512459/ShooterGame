using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerShooting : MonoBehaviour
{
    private AudioSource gunAudio;
    private Light gunLight;
    private LineRenderer gunLine;
    private ParticleSystem gunParticle;

    public float shootTime; // 发射子弹时间间隔
    public float effectDelayTime;  // 特效延迟时间

    private float currentShootTime; // 当前时间间隔

    // 开枪发射射线相关参数
    private Ray shootRay;
    private RaycastHit shootHit;
    private LayerMask shootMask;
    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponentInChildren<Light>();
        gunLine = GetComponentInChildren<LineRenderer>();
        gunParticle = GetComponentInChildren<ParticleSystem>();

        // 设置射线的命中层
        shootMask = LayerMask.GetMask("Enemy");
    }
    private void Update()
    {
        currentShootTime += Time.deltaTime;
        if (currentShootTime >= shootTime)
        {
            if (Input.GetButton("Fire1"))
            {
                currentShootTime = 0f;
                // 发射子弹
                Shoot();
            }
        }

        if (currentShootTime >= shootTime * effectDelayTime)
        {
            gunLight.enabled = false;
            gunLine.enabled = false;
        }
    }

    private void Shoot()
    {
        //Debug.Log(DateTime.Now.ToString("HH::mm::ss::ff"));
        gunAudio.Play();//播放枪声

        gunLight.enabled = true;//枪光亮

        gunParticle.Play(); //播放粒子特效

        //从枪口发射射线 获取射线命中的物体
        gunLine.SetPosition(0, transform.position);
        //gunLine.SetPosition(1, transform.position + transform.forward * 100);
        gunLine.enabled = true;

        // 发射射线 检测是否有物体被击中
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, 100, shootMask))
        {
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
}
