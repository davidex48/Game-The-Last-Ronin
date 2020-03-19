using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody2D))]
public class BetterMovement : MonoBehaviour
{
    public Animator animator;
<<<<<<< Updated upstream
=======
    [SerializeField]
    public float velocity;
>>>>>>> Stashed changes

    public float velocity;   
    Rigidbody2D rb;
    float horizontalMove = 10.0f;
    [SerializeField]
    float verticalForce = 6.6f;
    bool isGrounded;
    bool ableJump;
    [SerializeField]
    float fallMultiplier = 2.0f;
    [SerializeField]
    float lowJumpMultiplier = 3.5f;

    public static float velxKunai;//PAra sumar la V de player al kunai y asi evitamos ir mas rapidos que el kunai 
    public static float velyKunai;//

    void Start()
    {

    }

    //static Vector2 kunaiVel = new Vector2(velxKunai, velyKunai);
    private void Awake()
    {
        ableJump = false;
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        for (int i = 0; i < collider.Length; i++)
        {
           
            if (collider[i].gameObject.tag == "Ground")
            {
               
                ableJump = true;

            }
           // Debug.Log("THE COLLISION WAS WITH " + collider[i].gameObject.tag);
        }

        BetterJump();
        MoveCharacter();
        HandleLayers();
        //BetterJump();
    }



    // cuando se ejecuta Brackeys
 

    void MoveCharacter()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))      //Si no estoy en animacion de atacando se meve nornmal en los dos ejes X e Y
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);
            velxKunai = horizontalMove; //Variables que uso para darle la velocidad del player al kunai.
            velyKunai = rb.velocity.y;
            rb = GetComponent<Rigidbody2D>();//Sirve de algo?¿
           
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))       //Si estoy en animacion de ataque pongo el mov en exe X a 0 y lo matengo en Y para que cando ataque en el aire no flote.
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(0, rb.velocity.y); //Puedo dividir entre dos rb.velocity.y para que se reduzca un poco el descenso
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);
            velxKunai = 0;
            velyKunai = 0;
            rb = GetComponent<Rigidbody2D>();//Sirve de algo?¿
        }
     
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //Codigo espejo: Me orienta al personaje hacia donde estoy mirando. Cuando no estoy atacando funciona
            Vector3 characterScale = transform.localScale;
            if (Input.GetAxis("Horizontal") < 0)
            {
                characterScale.x = 2;
            }
            if (Input.GetAxis("Horizontal") > 0)

            {
                characterScale.x = -2;
            }
            transform.localScale = characterScale;
        }
        else//Cuando estoy atacando no se ejecuta codigo espejo i asi si intento moverme en direccion contraria no se me orienta al reves el PJ
        {
            return;
        }
    }

    
    void BetterJump()
    {
        
        //if (Input.GetButtonDown("Jump")) { Debug.Log("JUMP!"); }

        if (Input.GetButtonDown("Jump") && ableJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1 * verticalForce);
            animator.SetBool("IsJumping", true);
            //rb.velocity = Vector2.up * verticalForce;
        }
        else
        {
            ableJump = false;
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

        }
    }
   public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
        }
        animator.SetLayerWeight(1, 0);
    }
} 