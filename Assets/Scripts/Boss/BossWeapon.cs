using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public Animator animator;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask musashiLayer;

    private void Start()
    {
       // animator = GetComponent(Animator);
        //animator = Animator.GetC//animator.GetComponent<Boss>();
    }


    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(pos, attackRange, musashiLayer);   //Me detecta colisiones a partir de circulo con (centro, radio
        foreach (Collider2D musashi in hitEnemies)    //creo variable enemy y marco con ella a todo con lo que he detectado colision.
        {
            musashi.GetComponent<BetterMovement>().bossHit = true;
            Debug.Log("Detecta Ataque a mushasi");
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, musashiLayer);
        if (colInfo != null)
        {
            
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }

    private void FixedUpdate()
    {
        
        if (animator.GetBool("Attack")) Attack();
        //if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){ 
        //Attack(); 
        //Debug.Log("Entra!");
    }
}