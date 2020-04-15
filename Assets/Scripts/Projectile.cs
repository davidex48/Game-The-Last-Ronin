//Como llamar a una funcion de otra class(en este caso de Enemy) GetComponent<Enemy>().damageReceived(damage) !!!!!!!!!!!!!

//Como llamar variable de otra class sin que esta sea static   !!!!!!!!!!!!!!!  GetComponent<Enemy>().enemyHealth = 25;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    //bool condi = false;
    //float cooldown = 2;


    private bool isDestroyed;
    public GameObject enemy;
    public GameObject player;
    private Transform playerTrans;
    private Rigidbody2D kunaiRB;
    public float bulletSpeed = 1.5f;
    public float bulletLife;
    public static int damage;
    public int damageRef = 25;

    // Start is called before the first frame update



    void Awake()
    {
        isDestroyed = false;
        damage = damageRef;
        kunaiRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
        //velocity = BetterMovement.;
        enemy = GameObject.FindGameObjectWithTag("Enemy");  //NUEVOOOO

    }


    void Start()
    {
        if (playerTrans.localScale.x > 0)
        {

            kunaiRB.velocity = new Vector2(-bulletSpeed + BetterMovement.velxKunai, kunaiRB.velocity.y);    //Sumamos la V del player para no avanzar al kunai cuando nos movemos
            transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
        }
        if (playerTrans.localScale.x < 0)
        {
            kunaiRB.velocity = new Vector2(bulletSpeed + BetterMovement.velxKunai, kunaiRB.velocity.y);
            transform.localScale = new Vector3(-0.13f, 0.13f, 0.13f);
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
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }
        if (collision.tag == "Ground")
        {
            
            Destroy(gameObject);
        }

        isDestroyed = true;
    }

   
    /* Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);       //Detecta colision Con todo lo que tenga tag Enemy y me destruye el kunai. 
                                                                                         //Problemas al llamar funciones de Enemy porque no me las detecta.
     for (int i = 0; i < collider.Length; i++)
     {
         if (collider[i].gameObject.tag == "Enemy")
         {
             Destroy(gameObject);
             //enemy1.damageReceived(damage);  //Null Reference exception 
         }
     }*/
    //Si en destroy uso kunaiRB me elimina los components de la velocidad y el kunai se para pero no el objeto como tal!!!!!!!!!!!!!!!!!!!!


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy1 = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy1.damageReceived(damage);
        }
    }*/


    //Object.Destroy(GameObject, bulletLife);
    //Destroy(kunaiRB, bulletLife);
    //DestroyObject(kunaiRB, bulletLife);
    /*
     cooldown -= Time.deltaTime;
     if (cooldown <= 1.0f)
     {
         Destroy(kunaiRB, cooldown);
         cooldown = 2.0f;
         // condi = true;
     }*/
    /* if (condi)
     {
         Destroy(kunaiRB, 0.0f);
         condi = false;
     }*/
    //transform.Translate(0, speed * Time.deltaTime, 0);

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy1 = collision.gameObject.GetComponent<Enemy>())
            enemy1.damageReceived(damage);
            Debug.Log("THE COLLISION WAS WITH KUNAI");
        }*/

    // 
}
