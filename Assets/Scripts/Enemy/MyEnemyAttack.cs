using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackSpeed = 1.5f;

    private bool playerInRange = false;
    private float timer = 0f;
    private GameObject player;
    private MyPlayerHealth myPlayerHealth;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myPlayerHealth = player.GetComponent<MyPlayerHealth>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!myPlayerHealth.IsDead && playerInRange && timer >= attackSpeed)
        {
            myPlayerHealth.TakeDamage(attackDamage);
            timer = 0f;
        }
        if (myPlayerHealth.IsDead)
        {
            animator.SetTrigger("Idel");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
