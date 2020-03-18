using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    public static int enemyValue = 100;
    public static int enemyHealth = 100;

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kunai")
        {
            enemyHealth -= Projectile.damage;

            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log(enemyValue);//Me devuelve el numero de puntos que se han ganado por matar al Enemy
            }
        }
    }
        // Start is called before the first frame update

        void Start()

        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

    }

