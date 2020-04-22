using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    public static int enemyValue = 100;
    public int enemyHealth = 100;           //No hacerlo static.   

    //Static: Todos los objetos(gameObjects sean enemigos piedras o lo que sea) que contienen el script de la clase (Enemy) pueden modificar el valor. A demas este valor se ve modificado en todos los 
    //GameObjects que tienen attched el script de la clase. Por este motivo al atacar o lanzar kunai le bajaba la vida a todos los Onis(que todos contenian script enemy). Al quitarle static haces que 
    //cada oni tenga su propia enemyHealth.

    bool enemyChasing;
    public float desaggroRange;
    public float agroRange;
    //public int stoppingDistance;
    [SerializeField] private float damageValue1;
    //[SerializeField] private float actualLife;
    //[SerializeField] public int lifeAmount;
    [SerializeField] float speed = 0.05f;
    //[SerializeField] float pushMagnitude = 10.0f;
    private Transform player;
    [SerializeField] Image lifeBar;
    Rigidbody2D rb;

    public void damageReceived(int damageValue)
    {
        enemyHealth -= damageValue;

        //ManageEnemy.enemyHealth -= damageValue;

        //lifeBar.fillAmount = actualLife / lifeAmount;
        if (enemyHealth <= 0)
        {
            // SpawnManager.instance.removeEnemy(this);
            Destroy(gameObject);
            Debug.Log("Damage Funct CaC ON. Marramiau");
        }
    }
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChasing = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float distToPlayer = Vector2.Distance(transform.position, player.position);
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
        /*if(transform.position.x < player.position.x)
        {
        rb.velocity = new Vector2(speed, 0); //Enemigo a la izquierda del player, por eso muevo hacia la derecha (speed positiva)*/
        Vector3 dirVec = player.transform.position - transform.position;
        transform.position += dirVec.normalized * speed;

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
        /* else
         {
            // rb.velocity = new Vector2(-speed, 0); //Enemigo a la derecha del player. Muevo enemigo hacia la izquierda
             Vector3 dirVec = player.transform.position - transform.position;
             transform.position += dirVec.normalized * speed;
         }*/
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
