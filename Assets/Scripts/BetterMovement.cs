using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BetterMovement : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    float velocity;

    Rigidbody2D rb;
    float horizontalMove = 10.0f;
    [SerializeField]
    float verticalForce = 10.0f;
    bool isGrounded;
    bool ableJump;
    [SerializeField]
    float fallMultiplier = 4.5f;
    [SerializeField]
    float lowJumpMultiplier = 2.0f;

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
    }



    // cuando se ejecuta Brackeys
    private void Awake()
    {
        ableJump = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void MoveCharacter()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
        animator.SetFloat("Speed",Mathf.Abs (horizontalMove));
        Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
        Vector2 m_velocity = Vector2.zero;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);
    }

    
    void BetterJump()
    {
        
        //if (Input.GetButtonDown("Jump")) { Debug.Log("JUMP!"); }

        if (Input.GetButtonDown("Jump") && ableJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1 * verticalForce);
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

    

   
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -2;
        }
        if (Input.GetAxis("Horizontal")>0)

        {
            characterScale.x = 2;
        }
        transform.localScale = characterScale; 
    }
}