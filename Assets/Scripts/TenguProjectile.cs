using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenguProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); 
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyTenguProjectile();
        }
        void DestroyTenguProjectile()
        {
            Destroy(gameObject);
        }
    }
}

/*   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HellHound_Enemy")
        {
            collision.gameObject.GetComponent<HellHound>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        else if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        else if (collision.tag == "Ground")
        {

            Destroy(gameObject);
        }
        else if (collision.tag == "RangedTengu")
        {
            collision.gameObject.GetComponent<DemonRange>().damageReceived(damage);      //MANERA DE LLAMAR FUNCION DE OTRA CLASE GETCOMPONENT
            Destroy(gameObject);
        }

        isDestroyed = true;
    }*/
