using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.UI;
public class HellHound : MonoBehaviour
{

    private static int enemyValue = 50;
    public int enemyHealth = 50;           //No hacerlo static.   

    //Static: Todos los objetos(gameObjects sean enemigos piedras o lo que sea) que contienen el script de la clase (Enemy) pueden modificar el valor. A demas este valor se ve modificado en todos los 
    //GameObjects que tienen attched el script de la clase. Por este motivo al atacar o lanzar kunai le bajaba la vida a todos los Onis(que todos contenian script enemy). Al quitarle static haces que 
    //cada oni tenga su propia enemyHealth.
    [SerializeField] private float AttackTime;
    private float StartAttackTime;
    [SerializeField] private float Cooldown;
    private float resetCooldown;
    
    

    bool enemyChasing;
    float distToPlayer;
    public float desaggroRange = 8.5f;
    public float agroRange = 6.5f;
    
    private bool canAttack = true;
    

    //public int stoppingDistance;
    [SerializeField] private float damageValue1;
    //[SerializeField] private float actualLife;
    //[SerializeField] public int lifeAmount;
    [SerializeField]private float speed = 0.135f;
    
    [SerializeField] private float JumpAttackSpeed = 0.23f;
    //[SerializeField] float pushMagnitude = 10.0f;
    private Transform player;
    [SerializeField] Image lifeBar;
    private Rigidbody2D rb;
    private Animator anim;

    public void damageReceived(int damageValue)
    {
        enemyHealth -= damageValue;

        //ManageEnemy.enemyHealth -= damageValue;

        //lifeBar.fillAmount = actualLife / lifeAmount;
        if (enemyHealth <= 0)
        {
            // SpawnManager.instance.removeEnemy(this);
            Destroy(gameObject);
            //Debug.Log("Damage Funct CaC ON. Marramiau");
        }
    }
    // Use this for initialization
    private void Start()
    {
        speed = 0.115f;             //0.135
        JumpAttackSpeed = 0.2f;   // 0.23

        StartAttackTime = AttackTime;
        resetCooldown = Cooldown = 1.2f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChasing = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(anim.GetBool("isClose") + "Salto");

        distToPlayer = Vector2.Distance(transform.position, player.position);   //Calculamos dist del player

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
        
        else if (anim.GetBool("isRunning") || anim.GetBool("isClose"))//Si no estoy persiguiendo pero tengo animacion de correr entro aqui para desactivarla y que vuelva a iddle              
        {           
            StopChasingPlayer();    //Movimiento de patrulla implementarlo!!!!!!!!!!!!!!!!! 
            
        }
   
    }

    void ChasePlayer()
    {


        if (distToPlayer > 3.85f  || !canAttack)     //Si estoy en coldown de ataque siempre entrare aqui asi evito que salte/ataque continuamente 
        {
            anim.SetBool("isRunning", true);
            Vector3 dirVec = player.transform.position - transform.position;
            dirVec.y = 0.0f;
            transform.position += dirVec.normalized * speed;
            AttackTime = StartAttackTime;  //Asi evito arrastrar el tiempo del if de la linea 122 si salgo de su alcanze de salto   

            Cooldown -= Time.deltaTime;
            if (Cooldown <= 0)
            {
                canAttack = true;
            }
        }

        else if(distToPlayer <= 3.85f && canAttack)      //3.5
        {
            //canAttack = false;    Lo pondre en falso cuando pase el tiempo de ataque
            if (AttackTime > 0)
            {

                anim.SetBool("isClose", true);

                Vector3 dirVec; //= player.transform.position - transform.position;
                dirVec.x = player.transform.position.x - transform.position.x;
                dirVec.y = (player.transform.position.y - transform.position.y)/3;
                dirVec.z = player.transform.position.z - transform.position.z;     

                if (AttackTime > StartAttackTime/2)
                {
                  
                    
                 transform.position += (dirVec.normalized + Vector3.up * 0.4f) * JumpAttackSpeed;  //Movimineto mas rapido que el movimineto normal para dar sensacion de que se nos echa encima
                                                                                                   //El vector.Up * X(0.5) me modifica el valor en y, mientras mas grande mas salta
                }
                else
                {
                   
                    transform.position += (dirVec.normalized - Vector3.up * 0.4f) * JumpAttackSpeed;  //Movimineto mas rapido que el movimineto normal para dar sensacion de que se nos echa encima
                                                                                                      //El vector.Up * X me modifica el valor en y, mientras mas grande mas salta

                    //Jump attack Speed solo deveria afectar al eje X porque al salto tambien me lo multiplica
                }

                

                AttackTime -= Time.deltaTime;   //Le resto al tiempo de ataque el tiempo que llevo atacando. Esto me sirve para no 
            }

            else//Cuando a acabado el tiempo del ataque
            {
                canAttack = false;  //Pongo el flag de ataque a falso
                Cooldown = resetCooldown;   //Pongo el cooldown a su tiempo original para tener un delay entre ataques
                AttackTime = StartAttackTime;
                anim.SetBool("isClose", false);
            }
        }

        Vector3 characterScale = transform.localScale;
        if (player.transform.position.x <= transform.position.x)
        {
            characterScale.x = 3.2f;       
        }
        if (player.transform.position.x >= transform.position.x)

        {
            characterScale.x = -3.2f;
            
        }
        transform.localScale = characterScale;

    }
    void StopChasingPlayer()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isClose", false);
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
