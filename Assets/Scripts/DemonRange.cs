using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class DemonRange : MonoBehaviour
{

    private int enemyValue = 100;
    private int enemyHealth = 75;           //No hacerlo static.   

    private float timeToShoot;
    private float shootCooldown;
    public GameObject fireProjectile;

    private float distToPlayer; 
    private float stoppingDistance;
    private float retreatDistance;

    bool enemyChasing;
    public float desaggroRange;
    public float agroRange;

    [SerializeField] float speed;
    private Transform player;
    [SerializeField] Image lifeBar;
    Rigidbody2D rb;

    public void damageReceived(int damageValue)
    {
        enemyHealth -= damageValue;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Damage Funct CaC ON. Marramiau");
        }
    }

    void Start()
    {
        timeToShoot = shootCooldown = 1.65f;    // = 2?¿
        speed = 4.0f;
        stoppingDistance = 8.0f;
        retreatDistance = 7.2f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChasing = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            enemyChasing = true;
        }

        else if (distToPlayer > desaggroRange)
        {
            enemyChasing = false;
        }

        if (enemyChasing)
        {
            ChasePlayer();
            ShootPlayer();
        }

        else
        {
            StopChasingPlayer();
        }
        //Moviment vell
        /*
        
        */
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag == "Player")
         {
             Vector2 pushVector = (collision.gameObject.transform.position - transform.position).normalized;
             collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushVector * pushMagnitude, ForceMode2D.Impulse);
             Debug.Log("THE COLLISION WAS WITH PJ");
         }   
     }*/
    void ChasePlayer()
    {
        if(distToPlayer > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if(distToPlayer < stoppingDistance && distToPlayer > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (distToPlayer < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, - speed * Time.deltaTime);
        }

            /*Vector3 dirVec = player.transform.position - transform.position;
            transform.position += dirVec.normalized * speed;*/

            Vector3 characterScale = transform.localScale;
        if (player.transform.position.x <= transform.position.x)
        {
            characterScale.x = 1.3f;
        }
        if (player.transform.position.x >= transform.position.x)

        {
            characterScale.x = -1.3f;
        }
        transform.localScale = characterScale;
    }

    void ShootPlayer()
    {
        if (timeToShoot <= 0)
        {
            Instantiate(fireProjectile, transform.position, Quaternion.identity);
            timeToShoot = shootCooldown;
        }
        else timeToShoot -= Time.deltaTime;
    }
    void StopChasingPlayer()
    {
        return;
    }

}





/*
void Start()
{
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
}
// Update is called once per frame
void FixedUpdate()
{
    if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }*/
