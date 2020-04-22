using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_Player : MonoBehaviour
{
    public static bool onAttack;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public static int attackDmg = 100;
    // Start is called before the first frame update



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
        if (Input.GetButtonDown("Fire2"))
        {
            onAttack = true;
        }
    }

    void Attack()
    {
        if (onAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);   //Me detecta colisiones a partir de circulo con (centro, radio
            foreach (Collider2D enemy in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
            {
                enemy.GetComponent<Enemy>().damageReceived(attackDmg);  
                Debug.Log("Enemy Hit!!");

                
            }
            FindObjectOfType<AudioManager>().Play("PlayerAttack");
            animator.SetTrigger("Attack");  //Animacion del ataque.
            Bullet.canShoot = false;        //Asi controlo que no pueda disparar a la misma vez que ataco.

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

