﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    //public int enemyValue;
    [SerializeField] private float damageValue;
    [SerializeField] private float actualLife;
    [SerializeField] public int lifeAmount;
    [SerializeField] float speed = 0.05f;
    [SerializeField] float pushMagnitude = 10.0f;
    [SerializeField] public Transform player;
    [SerializeField] Image lifeBar;
    

    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kunai")
        {
            actualLife -= Projectile.damage;

            if (actualLife <= 0)
            {
                Destroy(gameObject);
                Debug.Log(enemyValue);//Me devuelve el numero de puntos que se han ganado por matar al Enemy
            }
        }
    }*/
     public void damageReceived(float damageValue)
     {
        /* actualLife -= damageValue;
         lifeBar.fillAmount = actualLife / lifeAmount;
         if (actualLife <= 0)
         {
            // SpawnManager.instance.removeEnemy(this);
             Destroy(gameObject);
             Debug.Log("Damage Funct Kunai On. Marramiau");
         }*/
     }
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /* void OnCollisionEnter2D(Collision2D CollisionKunai)
        {
            if (CollisionKunai.gameObject.tag.Equals("Kunai"))
            {
                damageReceived(kunai.damage);
            }
        }*/
        Vector3 dirVec = player.transform.position - transform.position;
        transform.position += dirVec.normalized * speed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 pushVector = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushVector * pushMagnitude, ForceMode2D.Impulse);
            Debug.Log("THE COLLISION WAS WITH PJ");
        }
       /* if (collision.gameObject.tag == "Kunai")
        {
            damageReceived(kunai.damage);
            Debug.Log("THE COLLISION WAS WITH KUNAI");
        }*/
    }
}

