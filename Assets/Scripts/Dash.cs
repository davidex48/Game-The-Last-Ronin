using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    private Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float cooldown;
    private float resetCooldown;
    private bool canDash;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        resetCooldown = cooldown; 
        canDash = true; //Inici dash ON
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction == 0 && canDash)  //direction y canDash OK
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift)) //GetKey: Mentre detecta que esta pressa. GetKeyDown: Es com un flanc, el primer impuls de premer tecla. GetKeyUp?
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 2;
            }
        }



        else  //Si no estan OK
        {
            if (dashTime <= 0)  //Asi mientras estoy en el dash no me cuenta el tiempo de cooldown, cuando salgo del dash si.
            {
                direction = 0;
                dashTime = startDashTime;
                //rb.velocity = Vector2.zero;
                cooldown -= Time.deltaTime;
                    if(cooldown <= 0)
                {
                    canDash = true;
                    cooldown = resetCooldown;
                }

            }
            else
            {
                dashTime -= Time.deltaTime;
                if(direction == 1)
                {
                    //rb.AddForce(Vector2.left * dashSpeed*100,0);
                    rb.velocity = new Vector2(-1 * dashSpeed, rb.velocity.y);
                }
                if (direction == 2)
                {
                    //rb.AddForce(Vector2.right * dashSpeed*100, 0);
                    //rb.velocity = Vector2.right * dashSpeed;
                    rb.velocity = new Vector2( dashSpeed, rb.velocity.y);
                }
                canDash = false; //
            }
        }
    }
}
