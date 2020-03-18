using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    //public int enemyValue;
    [SerializeField] private float damageValue;
    //[SerializeField] private float actualLife;
    //[SerializeField] public int lifeAmount;
    [SerializeField] float speed = 0.05f;
    [SerializeField] float pushMagnitude = 10.0f;
    [SerializeField] public Transform player;
    [SerializeField] Image lifeBar;
    

     public void damageReceived(int damageValue)
     {
        ManageEnemy.enemyHealth -= damageValue;
        
         //lifeBar.fillAmount = actualLife / lifeAmount;
         if (ManageEnemy.enemyHealth <= 0)
         {
            // SpawnManager.instance.removeEnemy(this);
             Destroy(gameObject);
             Debug.Log("Damage Funct CaC ON. Marramiau");
         }
     }
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirVec = player.transform.position - transform.position;
        transform.position += dirVec.normalized * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 pushVector = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushVector * pushMagnitude, ForceMode2D.Impulse);
            Debug.Log("THE COLLISION WAS WITH PJ");
        }
       /* if (collision.gameObject.tag == "Kunai")
        {
            damageReceived(kunai.damage);
            Debug.Log("THE COLLISION WAS WITH KUNAI");
        }*/
    }
}

