using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
   /* [SerializeField]
    float apertureAngle = 10.0f;
    [SerializeField]
    float attackSpeed = 1.0f;

    [SerializeField]
    int damage = 5;
    bool attacking = false;

    private void OnDrawGizmos()
    {
        Vector2 directVector = (new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
          Camera.main.ScreenToWorldPoint(Input.mousePosition).y) -
           new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;

        Gizmos.DrawLine(transform.position, directVector * 5.0f);
        Gizmos.DrawLine(transform.position,
      Quaternion.Euler(0f, 0f, apertureAngle) * directVector * 5.0f);
        Gizmos.DrawLine(transform.position,
      Quaternion.Euler(0f, 0f, -apertureAngle) * directVector * 5.0f);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseVector = Input.mousePosition;
        mouseVector = Camera.main.ScreenToWorldPoint(mouseVector);
        Vector2 direction = new Vector2(mouseVector.x - transform.position.x,
                                mouseVector.y - transform.position.y);

        if (!attacking)
        {
            transform.up = direction;
        }
        if (Input.GetMouseButtonDown(0) && !attacking)
        {


            direction = Quaternion.Euler(0.0f, 0.0f, apertureAngle) * direction;
            StartCoroutine(meleAttackC());

            attacking = true;
        }
    }


    IEnumerator meleAttackC()
    {

        GetComponentInChildren<Collider2D>().enabled = true;
        transform.rotation *= Quaternion.Euler(0.0f, 0.0f, apertureAngle);
        Quaternion lastRot = transform.rotation * Quaternion.Euler(0.0f, 0.0f, -apertureAngle * 2.0f);
        Quaternion firstRot = transform.rotation;

        float time = 0.0f;


        while (time <= 1.0f)
        {

            transform.rotation = Quaternion.Lerp(firstRot, lastRot, time);
            time += attackSpeed * Time.deltaTime;

            yield return null;
        }
        attacking = false;
        GetComponentInChildren<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().damageReceived(damage);
            other.GetComponent<Rigidbody2D>().AddForce((other.gameObject.transform.position - transform.position).normalized * 500.0f);
        }
    }*/
}
