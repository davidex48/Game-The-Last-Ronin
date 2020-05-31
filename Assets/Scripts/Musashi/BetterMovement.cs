using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if(this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Runing") && this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) FindObjectOfType<AudioManager>().Play("MusashiRunning");
public class BetterMovement : MonoBehaviour
{
    public GameObject SpawnPoint;   //Hazerlo dinamico así GameObject.FindGameObjectWithTag("Player")
                                    //GameObject.FindGameObjectWithTag("SpawnPoint")[0]

    //Si estoy colisionando rayo vertical con suelo v.y = 0, si rayo horizontal colisiona con ground v.x = 0;
    //Me he dado cuenta que la fuerza hace que mi personaje al caer desde muy alto penetre dentro de ground.

    public AudioClip stepSound;
    public AudioSource fuenteAudio;

    private const int STAMINE_REGEN = 35, MANA_REGEN = 5, MANA_CONSUMED = 37;   
    private const float NATURAL_SCALE_TIME = 1.0f, SLOWED_SCALE_TIME = 0.25f;
    private float maxStamine, velocity, mana, maxMana;
    public float stamine;   //No puede ser private porque se usa en script Bullet
    public Image stamineBar, manaBar;
    private Transform respawn;

    GameObject checkPointManager;
    public Animator animator;
    Rigidbody2D musashi;
    CapsuleCollider2D CapsulPlayerCol;               
    [SerializeField] private float horizontalMove, verticalForce, fallMultiplier, lowJumpMultiplier;
    [SerializeField]
    private bool onJump, isGrounded;
    public bool ableJump;
    public bool isDead;

    public float velxKunai, velyKunai;//Para sumar la V de player al kunai y asi evitamos ir mas rapidos que el kunai y para dar un ligero desvio en eje Y. No puede ser private porque se uasa en Projectile
    //Antes eran static pero se ha cambiado. Ahora se llama a la variable desde el script Projectile con: player.GetComponent<BetterMovement>().velxKunai;

    public bool timeSlowed, canSlowTime;
    private float fixedDeltaT;  //Copia de fixedDeltaTime para poder ponerlo en su valor original cuando vario timeScale 

    public void staminaReductor(int staminaValue)
    {
        stamine -= staminaValue;
        Debug.Log("Funcion Stamine ONN!!!!");      
    }


    void Start()
    {

        fuenteAudio = GetComponent<AudioSource>();

        checkPointManager = GameObject.FindGameObjectsWithTag("CheckpointManager")[0];

        fixedDeltaT = Time.fixedDeltaTime;
        horizontalMove = 10.0f;
        velocity = 10.0f; 
        verticalForce = 6.6f;
        fallMultiplier = 2.0f;
        lowJumpMultiplier = 3.5f;
        manaBar = GameObject.Find("ManaBarFill").GetComponent<Image>();
        stamineBar = GameObject.Find("StamineBarFill").GetComponent<Image>();
        stamine = maxStamine = 75;
        mana = maxMana = 40;
        respawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        velxKunai = velyKunai = 0.0f;
        canSlowTime = true;
        isDead = onJump = timeSlowed = false;
    }

    private void Awake()
    {
        
        ableJump = false;
        musashi = GetComponent<Rigidbody2D>();
        CapsulPlayerCol = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        //isDead = false;
        //STAMINE Regen

        if (stamine < maxStamine)
        {
            stamine += STAMINE_REGEN * Time.deltaTime;
        }

        //MANA Control & Regen

        if (mana >= maxMana / 4)    
        {
            canSlowTime = true;
        }

        if (mana < maxMana && !timeSlowed)
        {
            mana += MANA_REGEN * Time.deltaTime;
        }
        else if(timeSlowed && mana > 0)
        {
            mana -= MANA_CONSUMED * Time.deltaTime;    
        }
        else if(timeSlowed && mana <= 0)
        {
            if (mana <= 0)  
            {
                mana = 0;
            }
            timeSlowed = false;
            canSlowTime = false; //Como solo pongo en falso slowTime aqui consigo que si la mana me baja a zero no puedo volver a usar relentizacion hasta que me   MANA > 1/4     
            //pero si la paro antes si que puedo activar incluso si    MANA < 1/4
        }
       
        stamineBar.fillAmount = stamine / maxStamine;
        manaBar.fillAmount = mana / maxMana;
        
    }

    private void Update()
    {
        isDead = false;
        SlowTime();
         
        ableJump = false;

        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);//0.5
        for (int i = 0; i < collider.Length; i++)
        {

            if (collider[i].gameObject.tag == "Enemy" || collider[i].gameObject.tag == "HellHound_Enemy" || collider[i].gameObject.tag == "Pendul" || collider[i].gameObject.tag == "TenguProjectile")
            {          
                isDead = true;  //Flag que se usa en script de los enemigos para destruyrlos y en respawns para ponerlo a tru y volverlos a instanciar en su pos inicial
                if(checkPointManager != null && checkPointManager.GetComponent<CheckPointManager>().GetPos() != new Vector3(-1, -1, -1))
                {
                    this.transform.position = checkPointManager.GetComponent<CheckPointManager>().GetPos();
                }
                else
                {
                    musashi.transform.position = respawn.position;
                }
                    
              
            }
        }

        MoveCharacter();
  
        BetterJump();

        checkWallCol();
    }


    bool checkWallCol()
    {
        //return wallCol(Vector2.left) || wallCol(Vector2.right);      //Aqui no evalua les dos funcions
        bool toRetRight, toRetLeft;
        toRetRight = wallCol(Vector2.right);
        toRetLeft = wallCol(Vector2.left);          //Aqui evaluo les dues funcions tot i que al return si la primera es true ja retorna true perque es OR ||

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
        
        }

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))      //Si no estoy en animacion de atacando se meve nornmal en los dos ejes X e Y
        {
 
                horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;


            if(Input.GetAxisRaw("Horizontal") != 0 && musashi.velocity.y != 0)        //BUENO!!!!!!!!
            {
                    fuenteAudio.clip = stepSound;
                    fuenteAudio.Play();
            }

            Vector2 targetVelocity = new Vector2(horizontalMove, musashi.velocity.y);
                Vector2 m_velocity = Vector2.zero;
                musashi.velocity = Vector2.SmoothDamp(musashi.velocity, targetVelocity, ref m_velocity, Time.deltaTime); //0.05    //CAMBIAR POS CUERPO AL MOVERSE, PASAS PUNTO A Y PUNTO B I CALCULA VELOCIDAD CORRECTA PARA QUE LLEGUE ALD ESTIBNO
                                                                                                                         //musashi.velocity = targetVelocity;
                velxKunai = horizontalMove; //Variables que uso para darle la velocidad del player al kunai.
                velyKunai = musashi.velocity.y;

        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))  //Si estoy en animacion de ataque pongo el mov en exe X a 0 y lo matengo en Y para que cando ataque en el aire no flote.
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;
            Vector2 targetVelocity = new Vector2(0, musashi.velocity.y); //Puedo dividir entre dos musashi.velocity.y para que se reduzca un poco el descenso
            Vector2 m_velocity = Vector2.zero;
            musashi.velocity = Vector2.SmoothDamp(musashi.velocity, targetVelocity, ref m_velocity, Time.deltaTime);
            velxKunai = 0.0f;
            velyKunai = 0.0f;         
        }

        if (musashi.velocity.x == 0)// && musashi.velocity.y != 0)
        {
            fuenteAudio.clip = stepSound;
            fuenteAudio.Play();
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))   //Si estoy atacando y intento moverme no me gira el Pj
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
            onJump = false;
        }
        else if (!grounded())
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                animator.SetBool("IsJumping", true);
                //if (fuenteAudio.clip == stepSound) fuenteAudio.Stop();
            }
            else animator.SetBool("IsJumping", false);

            ableJump = false;
            onJump = true;
        }


        if (Input.GetButtonDown("Jump") && ableJump)       //grounded()
        {
            musashi.velocity = new Vector2(musashi.velocity.x, 1 * verticalForce);
            
            //musashi.velocity = Vector2.up * verticalForce;
        }
        //else


        //El raicast devuelve flag que actua como el ableJump = false. Rayo vertical muy pequeño que detecta colision con ground


        if (musashi.velocity.y < 0 && !ableJump)
        {

            if (fuenteAudio.clip == stepSound && onJump) fuenteAudio.Stop();
            musashi.velocity = new Vector2(musashi.velocity.x, musashi.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
            //Debug.Log("ENTROOOO");
            //musashi.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (musashi.velocity.y > 0 && !Input.GetButton("Jump")) //!Input.GetButton("Jump")
        {
            if (fuenteAudio.clip == stepSound && onJump) fuenteAudio.Stop();

            musashi.velocity = new Vector2(musashi.velocity.x, musashi.velocity.y + Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);

            //musashi.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

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
        void SlowTime()
        {

            if (Input.GetButtonDown("Fire3") && canSlowTime && !timeSlowed)
            {   //

                Time.timeScale = SLOWED_SCALE_TIME;
                Time.fixedDeltaTime *= Time.timeScale; //Para que los timers vayan a la par con el tiempo (cooldowns de mushasi y enemigos)
                timeSlowed = true;
            }
            else if((Input.GetButtonDown("Fire3") && timeSlowed) || !canSlowTime)
            {

                Time.timeScale = NATURAL_SCALE_TIME;
                Time.fixedDeltaTime = fixedDeltaT;
                //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
                timeSlowed = false;
            }       
    }

 
}



