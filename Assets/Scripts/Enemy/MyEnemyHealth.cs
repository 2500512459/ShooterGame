using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public int Health = 100;
    public bool IsDead = false;
    public AudioClip DeathClip;

    private bool isSiking = false;

    private AudioSource enemyAudioSource;
    private ParticleSystem enemyParticleSystem;
    private Animator enemyAnimator;
    private CapsuleCollider enemyCollider;
    private Rigidbody enemyRigidbody;
    private NavMeshAgent nav;
    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticleSystem = GetComponentInChildren<ParticleSystem>();
        enemyAnimator = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();
        enemyRigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(isSiking)
            transform.Translate(-transform.up * Time.deltaTime);
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        if (IsDead)
            return;

        // 受击音效
        enemyAudioSource.Play();

        // 粒子效果
        enemyParticleSystem.transform.position = hitPoint;
        enemyParticleSystem.Play();

        Health -= damage;
        if (Health <= 0)
        {
            // 死亡
            Death();
        }
    }

    private void Death()
    {
        IsDead = true;

        // 将碰撞器设置为触发器
        enemyCollider.isTrigger = true;
        // 停止物理检测减少开销
        enemyRigidbody.isKinematic = true;
        // 停止移动
        nav.enabled = false;
        // 播放死亡动画
        enemyAnimator.SetTrigger("Death");
        //  播放死亡音效
        enemyAudioSource.clip = DeathClip;
        enemyAudioSource.Play();
    }

    public void StartSinking()
    {
        isSiking = true;
        //销毁敌人
        Destroy(gameObject, 2f);
    }
}
