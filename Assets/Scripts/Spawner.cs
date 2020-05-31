﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float distToSpawn;
    [SerializeField]private GameObject enemy;
    //private Transform spawnPoint;   //Creo que no es necesario porque su posicion ya la conoce
    private Transform player;
    private bool canSpawn;


    void Start()
    {
        distToSpawn = 17.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canSpawn = true;
    }

    void Activate_Respawn(bool is_Player_Dead)
    {
        if (is_Player_Dead) canSpawn = true;
    }

    void FixedUpdate()
    {
        bool check_If_Player_Dead = player.GetComponent<BetterMovement>().isDead;

        Activate_Respawn(check_If_Player_Dead);

        if (canSpawn)
        {
            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distToPlayer > 0)   //Controlamos si el enemigo se encuentra a nuestra derecha
            {
                if (distToPlayer < distToSpawn)
                {
                    canSpawn = false;
                    Instantiate(enemy, transform.position, Quaternion.identity);
                }
            }
            else if (distToPlayer < 0)//Controlamos si estan a nuestra izquierda (dist negativa)
            {
                if (distToPlayer < (-distToPlayer))
                {
                    Instantiate(enemy, transform.position, Quaternion.identity);
                    canSpawn = false;
                }
            }
        }
        //else Destroy(this.gameObject);
    }
}
