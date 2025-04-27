using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
    public int health = 100;

    private AudioSource enemyAudioSource;
    private ParticleSystem enemyParticleSystem;
    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        // 受击音效
        enemyAudioSource.Play();

        // 粒子效果
        enemyParticleSystem.transform.position = hitPoint;
        enemyParticleSystem.Play();
        //health -= damage;
        //Debug.Log("Enemy Health: " + health);
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}
