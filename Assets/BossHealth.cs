using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    private int BosssHealth = 50;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void damageReceived(int damageValue)
    {
        if (isInvulnerable)
            return;

        BosssHealth -= damageValue;
        

            if (BosssHealth <= 10)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
            GetComponent<ParticleSystem>().Play();
            ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
            em.enabled = true;
        }

        if (BosssHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}