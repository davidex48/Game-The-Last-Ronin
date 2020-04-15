using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody2D))]
public class BetterMovement : MonoBehaviour
{
    public GameObject SpawnPoint;   //Hazerlo dinamico así GameObject.FindGameObjectWithTag("Player")
                                    //GameObject.FindGameObjectWithTag("SpawnPoint")[0]

//Si estoy colisionando rayo vertical con suelo v.y = 0, si rayo horizontal colisiona con ground v.x = 0;
//Me he dado cuenta que la fuerza hace que mi personaje al caer desde muy alto penetre dentro de ground.

    public Animator animator;
    public static int life;
    public float velocity;   
    Rigidbody2D rb;
    BoxCollider2D bc;
    float horizontalMove = 10.0f;
    [SerializeField]
    float verticalForce = 6.6f;
    bool isGrounded;
    public bool ableJump;
    [SerializeField]
    float fallMultiplier = 3.0f;
    [SerializeField]
    float lowJumpMultiplier = 3.5f;

    public static float velxKunai;//PAra sumar la V de player al kunai y asi evitamos ir mas rapidos que el kunai 
    public static float velyKunai;//

    void Start()
    {
        life = 100;
    }

    //static Vector2 kunaiVel = new Vector2(velxKunai, velyKunai);
    private void Awake()
    {
        ableJump = false;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();     //Deberia ser CapsuleCollider2D?¿  Pero no me deja llamarlo
    }

    private void Update()
    {
        ableJump = false;

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        for (int i = 0; i < collider.Length; i++)
        {

            if (collider[i].gameObject.tag == "Ground")
            {
                ableJump = true;
            }
            if (collider[i].gameObject.tag == "Enemy")               
            {
                //Destroy(gameObject);
                
                this.transform.position = SpawnPoint.transform.position;
            }
            if (collider[i].gameObject.tag == "Pendul")               
            {
                this.transform.position = SpawnPoint.transform.position;
                //Destroy(gameObject);
            }
        }

        MoveCharacter();
        //if (rb.velocity.y > asd) rb.velocity = new Vector2(rb.velocity.x, asd);   Para controlar que la V no sobrepase un limite (variable asd)
        BetterJump();

     
        //wallColRight();
        wallColLeft();
        HandleLayers();
        /*if (ableJump)//Para comprobar si de esta manera caigo 
        {      //Arreglar con RayCast
               //RayCastAll de distancia corta que si me detecta colision con ground no me permita saltar y tambien para que la velocidad en X sea 0 (para caer en los muros)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
        }*/


    }
    bool wallColRight()
    {
        Color rayColor;
        float dist = 0.2f;
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(bc.bounds.center, Vector2.right, bc.bounds.extents.x + dist);
        for (int i = 0; i < raycastHit.Length; i++)
        {

            if (raycastHit[i].collider.gameObject.tag == "Ground")
            {

                rayColor = Color.green;
                Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.center.y + dist), rayColor);
                return true;
            }
        }
        Debug.Log("RED");
        rayColor = Color.red;
        Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + dist), rayColor);
        return false;


    }

    bool wallColLeft()
    {
        Color rayColor;
        float dist = 0.2f;
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(bc.bounds.center, Vector2.left, - bc.bounds.extents.x - dist);
        for (int i = 0; i < raycastHit.Length; i++)
        {

            if (raycastHit[i].collider.gameObject.tag == "Ground")
            {

                rayColor = Color.green;
                Debug.DrawRay(bc.bounds.center, Vector2.down * (-bc.bounds.extents.y - dist), rayColor);
                return true;
            }
        }
        Debug.Log("RED");
        rayColor = Color.red;
        Debug.DrawRay(bc.bounds.center, Vector2.down * (-bc.bounds.extents.y - dist), rayColor);
        return false;


    }

    bool grounded()
    {
        Color rayColor;
        float dist = 0.2f;
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(bc.bounds.center, Vector2.down, bc.bounds.extents.y + dist);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            
            if (raycastHit[i].collider.gameObject.tag == "Ground")
            {
                //Debug.Log("GREEN");
                rayColor = Color.green;
                Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + dist), rayColor);
                return true;
            }


            /*if (raycastHit != null)    // if (raycastHit.collider != null)
            {
                
                rayColor = Color.green;
            }*/                                                                     
        }
        //Debug.Log("RED");
        rayColor = Color.red;
        Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + dist), rayColor);
        return false; // return raycastHit.collider != null;

        
    }

    void MoveCharacter()
    {
        //si vel > que maxVel 
        if (ableJump && grounded())
        {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))      //Si no estoy en animacion de atacando se meve nornmal en los dos ejes X e Y
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f); //Time.deltaTime    //CAMBIAR POS CUERPO AL MOVERSE, PASAS PUNTO A Y PUNTO B I CALCULA VELOCIDAD CORRECTA PARA QUE LLEGUE ALD ESTIBNO
            //rb.velocity = targetVelocity;
            velxKunai = horizontalMove; //Variables que uso para darle la velocidad del player al kunai.
            velyKunai = rb.velocity.y;
            //rb = GetComponent<Rigidbody2D>();//Sirve de algo?¿
           
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))       //Si estoy en animacion de ataque pongo el mov en exe X a 0 y lo matengo en Y para que cando ataque en el aire no flote.
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(0, rb.velocity.y); //Puedo dividir entre dos rb.velocity.y para que se reduzca un poco el descenso
            Vector2 m_velocity = Vector2.zero;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, 0.05f); 
            //rb.velocity = targetVelocity;
            velxKunai = 0;
            velyKunai = 0;
            //rb = GetComponent<Rigidbody2D>();//Sirve de algo?¿
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
        
        

        if (Input.GetButtonDown("Jump") && ableJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1 * verticalForce);
            animator.SetBool("IsJumping", true);
            ableJump = false;
            //rb.velocity = Vector2.up * verticalForce;
        }
        //else


        //El raicast devuelve flag que actua como el ableJump = false. Rayo vertical muy pequeño que detecta colision con ground


        if (rb.velocity.y < 0 && !ableJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            Debug.Log("ENTROOOO");
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) //!Input.GetButton("Jump")
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
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }*/
} 