using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Script que fa l'efecte mirall
    //GameObject script;
    public Transform player;
    float current_time, cooldown;
    public bool isFlipped = false;


    private void Start()
    {
        current_time = cooldown = 0.3f;
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public bool canAttack()
    {
        current_time -= Time.deltaTime;
        if (current_time <= 0)
        {
            current_time = cooldown;
            return true;
        }
        else return false;
    }
}
    /*private void FixedUpdate()
    {
        //if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            script.GetComponent<BossWeapon>().Attack();
        }
    }
}*/