using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_Player : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
  


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Attack();
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
