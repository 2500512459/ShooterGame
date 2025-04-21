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

    public float shootTime; // �����ӵ�ʱ����
    public float effectDelayTime;  // ��Ч�ӳ�ʱ��

    private float currentShootTime; // ��ǰʱ����

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponentInChildren<Light>();
        gunLine = GetComponentInChildren<LineRenderer>();
        gunParticle = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        currentShootTime += Time.deltaTime;
        if (currentShootTime >= shootTime)
        {
            if (Input.GetButton("Fire1"))
            {
                currentShootTime = 0f;
                // �����ӵ�
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
        gunAudio.Play();//����ǹ��

        gunLight.enabled = true;//ǹ����

        gunParticle.Play(); //����������Ч

        //��ǹ�ڷ������� ��ȡ�������е�����
        gunLine.SetPosition(0, transform.position);
        gunLine.SetPosition(1, transform.position + transform.forward * 100);
        gunLine.enabled = true;
    }
}
