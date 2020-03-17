using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    //bool condi = false;
    //float cooldown = 2;

    public Enemy enemy1;


    public GameObject enemy;
    public GameObject player;
    private Transform playerTrans;
    private Rigidbody2D kunaiRB;
    public float bulletSpeed = 1.5f;
    public float bulletLife;
    public static int damage;
    public int damageRef;

    // Start is called before the first frame update



    void Awake()
    {
        damage = damageRef;
        kunaiRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy");  //NUEVOOOO
    }



    void Start()
    {
        if (playerTrans.localScale.x > 0)
        {
            kunaiRB.velocity = new Vector2(bulletSpeed, kunaiRB.velocity.y);    //Sumarle la velocidad que lleva player para que nos e queden atras al crrer  dashear
            transform.localScale = new Vector3(-0.13f, 0.13f, 0.13f);
        }
        if (playerTrans.localScale.x < 0)
        {
            kunaiRB.velocity = new Vector2(-bulletSpeed, kunaiRB.velocity.y);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Destroy(gameObject, bulletLife);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
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
   


