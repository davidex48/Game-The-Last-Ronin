using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private float startDashTime;
    private int direction;
    [SerializeField] private float cooldown;
    private float resetCooldown;
    private bool canDash;
    public Animator animator;
    [SerializeField]private int stamineCost;

    // Start is called before the first frame update
    void Start()
    {
        dashSpeed = 38.5f;  //30.0f
        stamineCost = 25;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime = 0.135f;  //0.1         //Tiempo que dura el dash, que cuando entro en dash lo igualo para que siempre sea el mismo tiempo
        resetCooldown = cooldown = 0.1f; //0.2f
        canDash = true; //Inici dash ON
    }

  
    void Update()
    {
        float currentStamine = rb.GetComponent<BetterMovement>().stamine; //Leo variable stamina de BetterMovment
        


        if (direction == 0 && canDash && currentStamine >= stamineCost)  
        {
            

            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift)) //GetKey: Mentras detecta que esta pressa. GetKeyDown: Es com un flanc, el primer impuls de premer tecla. GetKeyUp: En el momento que la suelto
            {
                rb.GetComponent<BetterMovement>().staminaReductor(stamineCost); //Llamo a funcion que reduce stamina
                animator.SetBool("Dashing", true);
                animator.SetTrigger("Dash");
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                rb.GetComponent<BetterMovement>().staminaReductor(stamineCost); //Llamo a funcion que reduce stamina
                animator.SetBool("Dashing", true);
                animator.SetTrigger("Dash");
                direction = 2;
            }
        }



        else  //Si no estan OK
        {
            if (dashTime <= 0)  //Asi mientras estoy en el dash no me cuenta el tiempo de cooldown, cuando salgo del dash si.
            {
                animator.SetBool("Dashing", false);
                direction = 0;
                dashTime = startDashTime;
                //rb.velocity = Vector2.zero;
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    canDash = true;
                    cooldown = resetCooldown;
                }

            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    animator.SetTrigger("Dash");
                    //rb.AddForce(Vector2.left * dashSpeed*100,0);
                    rb.velocity = new Vector2(-1 * dashSpeed, rb.velocity.y);
                }
                if (direction == 2)
                {
                    animator.SetTrigger("Dash");
                    //rb.AddForce(Vector2.right * dashSpeed*100, 0);
                    //rb.velocity = Vector2.right * dashSpeed;
                    rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                }
                animator.SetBool("Dashing", false);
                canDash = false; //
            }
        }
    }
}