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

    private const int STAMINE_REGEN = 25;   //25
    private float maxStamine;
    public float stamine;
    public Image stamineBar;
    private Transform respawn;
    public Animator animator;
    public static int life;
    public float velocity;
    Rigidbody2D musashi;
    CapsuleCollider2D CapsulPlayerCol;               //Cambiar noms
    float horizontalMove;
    [SerializeField]
    float verticalForce;
    bool isGrounded;
    public bool ableJump;
    [SerializeField]
    float fallMultiplier;
    [SerializeField]
    float lowJumpMultiplier;

    public static float velxKunai;//Para sumar la V de player al kunai y asi evitamos ir mas rapidos que el kunai 
    public static float velyKunai;//


    public void staminaReductor(int staminaValue)
    {
        stamine -= staminaValue;
        Debug.Log("Funcion Stamine ONN!!!!");
        
    }
    void Start()
    {
        horizontalMove = 10.0f;
        velocity = 10.0f; 
        verticalForce = 6.6f;
        fallMultiplier = 2.0f;
        lowJumpMultiplier = 3.5f;
        life = 100;
        stamineBar = GameObject.Find("StamineBarFill").GetComponent<Image>();
        stamine = maxStamine = 75;
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    //static Vector2 kunaiVel = new Vector2(velxKunai, velyKunai);
    private void Awake()
    {
        ableJump = false;
        musashi = GetComponent<Rigidbody2D>();
        CapsulPlayerCol = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (stamine < 100)
        {
            stamine += STAMINE_REGEN * Time.deltaTime;
        }
        stamineBar.fillAmount = stamine / maxStamine;
    }

    private void Update()
    {

         
        ableJump = false;

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.65f);//0.5
        for (int i = 0; i < collider.Length; i++)
        {

             if (collider[i].gameObject.tag == "Enemy" || collider[i].gameObject.tag == "HellHound_Enemy" || collider[i].gameObject.tag == "Pendul")
            {
                //Destroy(gameObject);
                musashi.transform.position = respawn.position;
                //this.transform.position = SpawnPoint.transform.position;
            }
        }

        MoveCharacter();
        //if (musashi.velocity.y > asd) musashi.velocity = new Vector2(musashi.velocity.x, asd);   Para controlar que la V no sobrepase un limite (variable asd)
        BetterJump();

        checkWallCol(); //LLamar a esta funcion donde la necesite o hacerme una var tipo bool = a checkwallCool¿?¿?¿?¿

        HandleLayers();
        /*if (ableJump)//Para comprobar si de esta manera caigo 
        {      //Arreglar con RayCast
               //RayCastAll de distancia corta que si me detecta colision con ground no me permita saltar y tambien para que la velocidad en X sea 0 (para caer en los muros)
            musashi.velocity = new Vector2(musashi.velocity.x, musashi.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
        }*/

    
    
    }
    bool checkWallCol()
    {
        //return wallCol(Vector2.left) || wallCol(Vector2.right);      //Aqui no evalua les dos funcions
        bool toRetRight, toRetLeft;
        toRetRight = wallCol(Vector2.right);
        toRetLeft = wallCol(Vector2.left);          //Aqui evaluo les dues funcions tot i que al return si laprimera es true ja retorna true perque es OR ||

        return toRetRight || toRetLeft;
    }
    bool wallCol(Vector2 v)
    {
        //Hemos tenido que implementar otro rayCast en la aprte superior porque en algunos momentos de salto el rayCast inferior no colisionaba con el techo ("ground") y me permitia mover
        //en X y tenia de nuevo el bug de quedarme trabado en el aire al moverme.

        Color rayColor;
        float dist = 0.1f;
        RaycastHit2D[] raycastHitDown = Physics2D.RaycastAll(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y - CapsulPlayerCol.size.y - 0.01f, CapsulPlayerCol.bounds.center.z), v, CapsulPlayerCol.bounds.extents.x + dist);
        for (int i = 0; i < raycastHitDown.Length; i++)

        {

            if (raycastHitDown[i].collider.gameObject.tag == "Ground")
            {
                rayColor = Color.magenta;
                Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y - CapsulPlayerCol.size.y - 0.01f, CapsulPlayerCol.bounds.center.z), v * (CapsulPlayerCol.bounds.extents.x + dist), rayColor);
                return true;
            }
        }



        RaycastHit2D[] raycastHitUp = Physics2D.RaycastAll(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y + CapsulPlayerCol.size.y, CapsulPlayerCol.bounds.center.z), v, CapsulPlayerCol.bounds.extents.x + dist);
        for (int i = 0; i < raycastHitUp.Length; i++)

        {

            if (raycastHitUp[i].collider.gameObject.tag == "Ground")
            {
                rayColor = Color.magenta;
                Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y + CapsulPlayerCol.size.y + 0.01f, CapsulPlayerCol.bounds.center.z), v * (CapsulPlayerCol.bounds.extents.x + dist), rayColor);
                return true;
            }
        }

        rayColor = Color.cyan;
        //RayDown
        Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y - CapsulPlayerCol.size.y - 0.01f, CapsulPlayerCol.bounds.center.z), v * (CapsulPlayerCol.bounds.extents.x + dist), rayColor); //No hace falta que sea negativo imagino porque direccion la coje de los vectors
        //RayDown
        Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x, CapsulPlayerCol.bounds.center.y + CapsulPlayerCol.size.y + 0.01f, CapsulPlayerCol.bounds.center.z), v * (CapsulPlayerCol.bounds.extents.x + dist), rayColor);
        return false;


    }


    bool grounded() //Doblar rayCast de salto porque en extremos de un saliente no me deja saltar porque no detecta
    {
        Color rayColor;
        float dist = 0.15f;  //0.2
        /*RaycastHit2D[] raycastHit = Physics2D.RaycastAll(CapsulPlayerCol.bounds.center, Vector2.down, CapsulPlayerCol.bounds.extents.y + dist);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            
            if (raycastHit[i].collider.gameObject.tag == "Ground")
            {
                
                rayColor = Color.green;
                Debug.DrawRay(CapsulPlayerCol.bounds.center, Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
                return true;
            }                                                                     
        }
        
        rayColor = Color.red;
        Debug.DrawRay(CapsulPlayerCol.bounds.center, Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
        return false; // return raycastHit.collider != null;*/

        RaycastHit2D[] raycastHitLeft = Physics2D.RaycastAll(new Vector3(CapsulPlayerCol.bounds.center.x - CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down, CapsulPlayerCol.bounds.extents.y + dist);
        for (int i = 0; i < raycastHitLeft.Length; i++)
        {

            if (raycastHitLeft[i].collider.gameObject.tag == "Ground")
            {

                rayColor = Color.green;
                Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x - CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
                return true;
            }
        }



        RaycastHit2D[] raycastHitRight = Physics2D.RaycastAll(new Vector3(CapsulPlayerCol.bounds.center.x + CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down, CapsulPlayerCol.bounds.extents.y + dist);
        for (int i = 0; i < raycastHitRight.Length; i++)
        {

            if (raycastHitRight[i].collider.gameObject.tag == "Ground")
            {

                rayColor = Color.green;
                Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x + CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
                return true;
            }
        }

        rayColor = Color.red;

        Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x + CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
        Debug.DrawRay(new Vector3(CapsulPlayerCol.bounds.center.x - CapsulPlayerCol.size.x, CapsulPlayerCol.bounds.center.y, CapsulPlayerCol.bounds.center.z), Vector2.down * (CapsulPlayerCol.bounds.extents.y + dist), rayColor);
        return false; // return raycastHit.collider != null;




    }




    void MoveCharacter()
    {
        //si vel > que maxVel 
        if (ableJump && grounded())     //Dejarla sola esta condicion grounded?      
        {
            
            musashi.velocity = new Vector2(musashi.velocity.x, 0);
            //Debug.Log("ABLE JUMP && GROUNDED ONN");
            FindObjectOfType<AudioManager>().Play("MusashiRunning");
        }

      


        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))      //Si no estoy en animacion de atacando se meve nornmal en los dos ejes X e Y
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(horizontalMove, musashi.velocity.y);
            Vector2 m_velocity = Vector2.zero;
            musashi.velocity = Vector2.SmoothDamp(musashi.velocity, targetVelocity, ref m_velocity, Time.deltaTime); //0.05    //CAMBIAR POS CUERPO AL MOVERSE, PASAS PUNTO A Y PUNTO B I CALCULA VELOCIDAD CORRECTA PARA QUE LLEGUE ALD ESTIBNO
            //musashi.velocity = targetVelocity;
            velxKunai = horizontalMove; //Variables que uso para darle la velocidad del player al kunai.
            velyKunai = musashi.velocity.y;
            //musashi = GetComponent<Rigidbody2D>();//Sirve de algo?¿

            FindObjectOfType<AudioManager>().Play("MusashiRunning");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))  //Si estoy en animacion de ataque pongo el mov en exe X a 0 y lo matengo en Y para que cando ataque en el aire no flote.
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(0, musashi.velocity.y); //Puedo dividir entre dos musashi.velocity.y para que se reduzca un poco el descenso
            Vector2 m_velocity = Vector2.zero;
            musashi.velocity = Vector2.SmoothDamp(musashi.velocity, targetVelocity, ref m_velocity, Time.deltaTime);
            //musashi.velocity = targetVelocity;
            velxKunai = 0;
            velyKunai = 0;
            //musashi = GetComponent<Rigidbody2D>();//Sirve de algo?¿
            //rb = GetComponent<Rigidbody2D>();//Sirve de algo?¿
            FindObjectOfType<AudioManager>().Play("MusashiRunning");
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

        if (grounded())
        {
            animator.SetBool("IsJumping", false);
            ableJump = true;            
        }
        else if (!grounded())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                animator.SetBool("IsJumping", true);
            }
            else animator.SetBool("IsJumping", false);

            ableJump = false;
        }


        if (Input.GetButtonDown("Jump") && ableJump)       //grounded()
        {
            //animator.SetTrigger("takeOf");
            
            

            musashi.velocity = new Vector2(musashi.velocity.x, 1 * verticalForce);       
            
            //musashi.velocity = Vector2.up * verticalForce;
        }
        //else


        //El raicast devuelve flag que actua como el ableJump = false. Rayo vertical muy pequeño que detecta colision con ground


        if (musashi.velocity.y < 0 && !ableJump)
        {
            
            
            musashi.velocity = new Vector2(musashi.velocity.x, musashi.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            //Debug.Log("ENTROOOO");
            //musashi.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (musashi.velocity.y > 0 && !Input.GetButton("Jump")) //!Input.GetButton("Jump")
        {
            
            
            musashi.velocity = new Vector2(musashi.velocity.x, musashi.velocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);

            //musashi.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

        }

        if (wallCol(Vector2.left) && !ableJump)     //Cuando colisiono con muro izquierdo y no puedo saltar, si mi v < 0 (me muevo hacia el muro con el que colisiono) 
        {
            if (musashi.velocity.x <= 0)
                musashi.velocity = new Vector2(0, musashi.velocity.y);
        }
        if (wallCol(Vector2.right) && !ableJump)
        {
            if (musashi.velocity.x >= 0)
                musashi.velocity = new Vector2(0, musashi.velocity.y);
        }

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






