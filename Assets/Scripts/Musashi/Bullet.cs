using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   //AudioClip throwKunai;
   //AudioSource fuenteAudio;

    public static bool canShoot;    //Publica porque attack_player necesita acceder a esta variable.
    private float coldownAfterAttack; 
    float resetColdownAfterAttack;
    public GameObject posicionInicialKunai;
    public GameObject Projectile;
    public GameObject player;
    public int stamineShotCost;
    void Start()
    {
        //fuenteAudio = GetComponent<AudioSource>();
        stamineShotCost = 25;
        player = GameObject.FindGameObjectWithTag("Player");     
        coldownAfterAttack = 0.4f;
        resetColdownAfterAttack = coldownAfterAttack;
        canShoot = true;
    }

    void Update()
    {
        ShootAndDelayAfterAttack();
    }

    void ShootAndDelayAfterAttack()
    {
        float currentStamine = player.GetComponent<BetterMovement>().stamine;   //leo variable stamina de player

        if (canShoot && currentStamine >= stamineShotCost)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)//&& elapsedTime > fireRate)
            {
                player.GetComponent<BetterMovement>().staminaReductor(stamineShotCost); //Llamo a funcion de Bettermovment que me reduce la stamina
                Instantiate(Projectile, posicionInicialKunai.transform.position, posicionInicialKunai.transform.rotation); //Me crea el kunai
            }
        }
        else
        {
            if (coldownAfterAttack > 0)
                coldownAfterAttack -= Time.deltaTime;

            else
            {
                canShoot = true;
                coldownAfterAttack = resetColdownAfterAttack;
            }

        }
    }
}