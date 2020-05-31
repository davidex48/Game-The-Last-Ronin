using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class attack_Player : MonoBehaviour
{
    public bool onAttack;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public LayerMask HellHoundLayers;
    public LayerMask RangedTenguLayer;
    public LayerMask BossLayer;
    float resetColdownAfterAttack;
    float coldownAfterAttack;
    public static int attackDmg = 100;
    //public GameObject wolf;
    //public GameObject demon;

    // Start is called before the first frame update

    private void Start()
    {
        resetColdownAfterAttack = coldownAfterAttack = 0.8f;

    }

    // Update is called once per frame
    void Update()//El bucle se ejecuta con una frequencia bastante mas alta que FixedUpdate. 
    {
        InputAttack();
    }
    void FixedUpdate()
    {
        Attack();
        ResetValues();
    }


    void InputAttack()
    {
        resetColdownAfterAttack -= Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && resetColdownAfterAttack <= 0)
        {           
            onAttack = true;
            resetColdownAfterAttack = coldownAfterAttack;
        }        
    }

    void Attack()
    {
        

        if (onAttack)
        {
            animator.SetBool("IsJumping", false);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);   //Me detecta colisiones a partir de circulo con (centro, radio
            foreach (Collider2D enemy in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
            {                                   
                enemy.GetComponent<Enemy>().damageReceived(attackDmg);               
            }

            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, HellHoundLayers);
            foreach (Collider2D HellHound_enemy in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
            {
                HellHound_enemy.GetComponent<HellHound>().damageReceived(attackDmg);
            }
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, RangedTenguLayer);
            foreach (Collider2D DemonRange in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
            {
                DemonRange.GetComponent<DemonRange>().damageReceived(attackDmg);
            }
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossLayer);
            foreach (Collider2D Boss in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
            {
                Boss.GetComponent<BossHealth>().damageReceived(attackDmg);
            }


            /* Collider2D[] hitHellHound = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, HellHoundLayers);   //Me detecta colisiones a partir de circulo con (centro, radio
             foreach (Collider2D HellHound_enemy in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
             {

                 HellHound_enemy.GetComponent<HellHound>().damageReceived(attackDmg);
             }*/

            /* foreach (Collider2D demon in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
             {
                 demon.GetComponent<Enemy>().damageReceived(attackDmg);
             }

             foreach (Collider2D wolf in hitEnemies)
             {

             wolf.GetComponent<HellHound>().damageReceived(attackDmg);

             }*/
            /* if (Collider2D.FindObjectOfType<Enemy>())
             {
                 enemy.GetComponent<Enemy>().damageReceived(attackDmg);
                 Debug.Log("Enemy Hit!!");
             }
             if (hitEnemies.OfType<Enemy>()//.tag<HellHound>())
             {
                 enemy.GetComponent<HellHound>().damageReceived(attackDmg);
                 Debug.Log("Enemy Hit!!");
             }*/


            FindObjectOfType<AudioManager>().Play("PlayerAttack"); //Sonido del ataque
            
            animator.SetTrigger("Attack");  //Animacion del ataque.
            Bullet.canShoot = false;        //Pongo el flag que me permite disparar a false en el script Bullet!!! Asi controlo que no pueda disparar a la misma vez que ataco.

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void ResetValues()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            onAttack = false;
        }
    }
}

