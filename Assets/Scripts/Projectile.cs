using UnityEngine;

public class Projectile : MonoBehaviour
{
    //bool condi = false;
    //float cooldown = 2;
    public GameObject player;
    private Transform playerTrans;
    private Rigidbody2D kunaiRB;
    public float bulletSpeed = 1.5f;
    public float bulletLife;
    // Start is called before the first frame update

    void Awake()
    {
        kunaiRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;

    }


    void Start()
    {
        if (playerTrans.localScale.x > 0)
        {
            kunaiRB.velocity = new Vector2(bulletSpeed, kunaiRB.velocity.y);
            transform.localScale = new Vector3(-0.13f, 0.13f, 0.13f);
        }
        if (playerTrans.localScale.x < 0)
        {
            kunaiRB.velocity = new Vector2(-bulletSpeed, kunaiRB.velocity.y);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Si en destroy uso kunaiRB me elimina los components de la velocidad y el kunai se para pero no el objeto como tal!!!!!!!!!!!!!!!!!!!!
        Destroy(gameObject, bulletLife);
        //Object.Destroy(GameObject, bulletLife);
        //Destroy(kunaiRB, bulletLife);
        //DestroyObject(kunaiRB, bulletLife);
        /*
         cooldown -= Time.deltaTime;

         if (cooldown <= 1.0f)
         {
             Destroy(kunaiRB, cooldown);
             cooldown = 2.0f;
             // condi = true;
         }*/
        /* if (condi)
         {
             Destroy(kunaiRB, 0.0f);
             condi = false;
         }*/
        //transform.Translate(0, speed * Time.deltaTime, 0);






    }
}
