using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    [SerializeField] public float actualLife;
    [SerializeField] float lifeAmount = 100;
    [SerializeField] float speed = 0.05f;
    [SerializeField] float pushMagnitude = 10.0f;
    [SerializeField] public Transform player;
    [SerializeField] Image lifeBar;

    public void damageReceived(float damageValue)
    {
        actualLife -= damageValue;
        lifeBar.fillAmount = actualLife / lifeAmount;
        if (actualLife <= 0)
        {
           // SpawnManager.instance.removeEnemy(this);
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        actualLife = lifeAmount;
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
        }
    }
}

