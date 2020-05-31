using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    float current_time, cooldown;
    public AudioClip hitSound;
    AudioSource fuenteAudio;
    public GameObject endPortal;
    public GameObject TempDeadSound;
    private float bossMaxHealth, bossHealth;

    //public ParticleSystem raged;
    public GameObject deathEffect;
    public Image hpBar;
    public bool isInvulnerable = false;


    void Start()
    {
        current_time = cooldown = 1.8f;
        fuenteAudio = GetComponent<AudioSource>();
        bossMaxHealth = bossHealth = 300;
        hpBar = GameObject.Find("FillHealth").GetComponent<Image>();
        //raged = ParticleSystem.tag

    }

        public void damageReceived(int damageValue)
    {
        fuenteAudio.clip = hitSound;    
        fuenteAudio.Play();
        if (isInvulnerable)
            return;

        bossHealth -= damageValue;

        hpBar.fillAmount = bossHealth / bossMaxHealth;

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
            Instantiate(endPortal, this.transform.position, this.transform.rotation);
            Instantiate(TempDeadSound, this.transform.position, this.transform.rotation);
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}