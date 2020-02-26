using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BetterMovement : MonoBehaviour
{

    [SerializeField]
    float velocity;

    Rigidbody2D rb;
    float horizontalMove = 10.0f;
    [SerializeField]
    float verticalForce = 10.0f;
    bool isGrounded;
    bool ableJump;

    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2.0f;

    private void FixedUpdate()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        for (int i = 0; i < collider.Length; i++)
        {
            //Debug.Log("THE COLLISION WAS WITH " + collider[i].gameObject.tag);
            if (collider[i].gameObject.tag == "Ground")
            {
                ableJump = true;
            }
        }

        BetterJump();
        MoveCharacter();
    }



    // cuando se ejecuta Brackeys
    private void Awake()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void MoveCharacter()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
        Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
        Vector2 m_velocity = Vector2.zero;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);
    }

    
    void BetterJump()
    {
        //Debug.Log("MIAU " + ableJump); if (Input.GetButtonDown("Jump")) { Debug.Log("JUMP!"); }

        if (Input.GetButtonDown("Jump") && ableJump)
        {
            rb.velocity = Vector2.up * verticalForce;
        }
        else
        {
            ableJump = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1)
                * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))

        {
            rb.velocity += Vector2.up * Physics2D.gravity.y *
                  (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

        }
    }

    //rb+= rb +
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

    }
}