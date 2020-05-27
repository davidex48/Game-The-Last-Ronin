﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    private float bossMaxHealth, bossHealth;

    //public ParticleSystem raged;
    public GameObject deathEffect;
    public Image hpBar;
    public bool isInvulnerable = false;


    void Start()
    {
        bossMaxHealth = bossHealth = 200;
        hpBar = GameObject.Find("FillHealth").GetComponent<Image>();
        /*raged = ParticleSystem.tag
        player = GameObject.FindGameObjectWithTag("Player").transform;*/
    }

        public void damageReceived(int damageValue)
    {
        if (isInvulnerable)
            return;

        bossHealth -= damageValue;
        //hpBar.fillAmount -= (float)damageValue / 100;
        hpBar.fillAmount = bossHealth / bossMaxHealth;//-= 0.25f;//

    }
    private void FixedUpdate()
    {
        

        if (bossHealth <= 10)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
            //GetComponent<ParticleSystem>().Play();
            //ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
            //em.enabled = true;
        }

        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}