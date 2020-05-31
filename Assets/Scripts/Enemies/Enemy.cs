using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public AudioClip hitSound;
    AudioSource fuenteAudio;

    public static int enemyValue = 100;
    public int enemyHealth = 100;           //No hacerlo static.   

    //Static: Todos los objetos(gameObjects sean enemigos piedras o lo que sea) que contienen el script de la clase (Enemy) pueden modificar el valor. A demas este valor se ve modificado en todos los 
    //GameObjects que tienen attched el script de la clase. Por este motivo al atacar o lanzar kunai le bajaba la vida a todos los Onis(que todos contenian script enemy). Al quitarle static haces que 
    //cada oni tenga su propia enemyHealth.

    public GameObject TempDeadSound;
    bool enemyChasing;
    public float desaggroRange;
    public float agroRange;
    [SerializeField] float speed;
    private Transform player;
    [SerializeField] Image lifeBar;
    private Rigidbody2D rb;

    public void damageReceived(int damageValue)
    {
        enemyHealth -= damageValue;
        fuenteAudio.clip = hitSound;
        fuenteAudio.Play(); ;

        //lifeBar.fillAmount = actualLife / lifeAmount;
        if (enemyHealth <= 0)
        {
            Instantiate(TempDeadSound, this.transform.position, this.transform.rotation); //Me crea el kunai
            // SpawnManager.instance.removeEnemy(this);
            Destroy(gameObject);
            Debug.Log("Damage Funct CaC ON. Marramiau");
        }
    }

    private void Take_To_Respawn_Pos(bool isPlayerDead)
    {
        if (isPlayerDead) Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        speed = 4.0f; // antes de multiplicar speed por deltaTime speed = 0.05f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChasing = false;
        rb = GetComponent<Rigidbody2D>();

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kunai") { fuenteAudio.clip = hitSound; fuenteAudio.Play(); }

    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        bool check_If_Player_Dead = player.GetComponent<BetterMovement>().isDead;

        Take_To_Respawn_Pos(check_If_Player_Dead);  //Si el jugador muere elimino al enemigo. Con el mismo flagtmb se me reactiva respawn y genero otro enemigo en la pos del respawn

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

        //Vector3 dirVec = player.transform.position - transform.position;
        //transform.position += dirVec.normalized * speed;  Asi se movia antes pero habia conflicto con la relentizacion y multiplicamos speed * Yime.deltaTime para solucionarlo.

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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
