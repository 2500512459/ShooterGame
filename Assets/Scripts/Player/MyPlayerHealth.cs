using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{
    public int Health = 100;
    public bool IsDead = false;

    public AudioClip AudioClip;

    private AudioSource audioSource;
    private Animator animator;
    private PlayerMovement playerMovement;
    private MyPlayerShooting myPlayerShooting;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        myPlayerShooting = GetComponentInChildren <MyPlayerShooting>();
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
            return;
        Health -= damage;

        audioSource.Play();

        if (Health <= 0)
        {
            IsDead = true;
            Death();
        }
    }
    private void Death()
    {
        //  ²¥·ÅËÀÍöÒôÐ§
        audioSource.clip = AudioClip;
        audioSource.Play();

        // ²¥·ÅËÀÍö¶¯»­
        animator.SetTrigger("Death");

        // Í£Ö¹ÒÆ¶¯, Í£Ö¹Éä»÷
        playerMovement.enabled = false;
        myPlayerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
