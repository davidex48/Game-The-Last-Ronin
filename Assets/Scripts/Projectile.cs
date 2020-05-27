//Como llamar a una funcion de otra class(en este caso de Enemy) GetComponent<Enemy>().damageReceived(damage) !!!!!!!!!!!!!

//Como llamar variable de otra class sin que esta sea static   !!!!!!!!!!!!!!!  GetComponent<Enemy>().enemyHealth = 25;

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    private bool isDestroyed;
    public GameObject enemy;
    public GameObject HellHound;
    public GameObject player;
    public GameObject boss;
    private Transform playerTrans;
    private Rigidbody2D kunaiRB;
    public float bulletSpeed = 1.5f;
    public float bulletLife;
    public int damage;  //Antes era static
    public int damageRef = 25;

    void Awake()
    {
        isDestroyed = false;
        damage = damageRef;
        kunaiRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");        
        playerTrans = player.transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        HellHound = GameObject.FindGameObjectWithTag("HellHound_Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");

        

    }


    void Start()
    {
        float rectifyVelX = player.GetComponent<BetterMovement>().velxKunai;
        float rectifyVelY = player.GetComponent<BetterMovement>().velyKunai / 2.5f; //Para quen el kunai tenga un ligero movimiento en eje y si estoy saltando

        if (playerTrans.localScale.x > 0)
        {

            //kunaiRB.velocity = new Vector2(-bulletSpeed + BetterMovement.velxKunai, kunaiRB.velocity.y);    
            kunaiRB.velocity = new Vector2(-bulletSpeed + rectifyVelX, kunaiRB.velocity.y + rectifyVelY);  //Sumamos la V del player para no avanzar al kunai cuando nos movemos. Tambien sumamos un desvio en Y.
            if (kunaiRB.velocity.x == 0) //Asi evito BUG que cuando me muevo a izquierda y derecha rapidamente y disparo el kunai se quede quieto. 
            {
                kunaiRB.velocity = new Vector2(-bulletSpeed, kunaiRB.velocity.y);  
            }
            //kunaiRB.velocity = new Vector2(-bulletSpeed + rectifyVelX, kunaiRB.velocity.y);    //Sumamos la V del player para no avanzar al kunai cuando nos movemos
            transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
        }
        if (playerTrans.localScale.x < 0)
        {
            //kunaiRB.velocity = new Vector2(bulletSpeed + BetterMovement.velxKunai, kunaiRB.velocity.y);
            kunaiRB.velocity = new Vector2(bulletSpeed + rectifyVelX, kunaiRB.velocity.y + rectifyVelY);
            if (kunaiRB.velocity.x == 0) //Asi evito BUG que cuando me muevo a izquierda y derecha rapidamente y disparo el kunai se quede quieto. 
            {
                kunaiRB.velocity = new Vector2(bulletSpeed, kunaiRB.velocity.y);
            }
            //kunaiRB.velocity = new Vector2(bulletSpeed + rectifyVelY, kunaiRB.velocity.y);
            transform.localScale = new Vector3(-0.13f, 0.13f, 0.13f);
            //player.gameObject.GetComponent<BetterMovement>().velxKunai;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDestroyed)
        {
            Destroy(gameObject, bulletLife);
            //Si no a chocado la bala y no se a destruido la destruyra al acabar el tiempo de bulletLife. Tengo que controlar si se a destruydo o se crashea porque intenta destruir algo que ya no existe
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HellHound_Enemy")
        {
            collision.gameObject.GetComponent<HellHound>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        else if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        else if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossHealth>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        else if (collision.tag == "Ground")
        {

            Destroy(gameObject);
        }
        else if (collision.tag == "RangedTengu")
        {
            collision.gameObject.GetComponent<DemonRange>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        isDestroyed = true;
    }
}