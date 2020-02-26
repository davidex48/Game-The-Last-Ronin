using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BetterMovement : MonoBehaviour
{

    [SerializeField]
    float velocity;

    Rigidbody2D rb;
    float horizontalMove;
    [SerializeField]
    float verticalForce = 10.0f;
    bool isGrounded;

    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2.0f;
    private void Awake()
    // cuando se ejecuta Brackeys
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void moveCharacter()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
        Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
        Vector2 m_velocity = Vector2.zero;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f);

    }
    bool wasPressed;
    void BetterJump()
    // Use this for initialization
    {


        if (Input.GetButtonDown("Jump") && wasPressed)
        {
            rb.velocity = Vector2.up * verticalForce;

        }
        else
        {
            wasPressed = false;
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

    private void FixedUpdate()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        for (int i = 0; i < collider.Length; i++)

        {
            if (collider[i].gameObject.tag == "Ground")

            {
                wasPressed = true;

            }
        }



        BetterJump(); ;
        moveCharacter();

    }
    // Update is called once per frame
    void Update()
    {

    }
}