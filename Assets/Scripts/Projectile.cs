using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D kunaiRB;
    public float bulletSpeed = 1.5f;
    // Start is called before the first frame update

    void Awake()
    {
        kunaiRB = GetComponent<Rigidbody2D>();

    }


    void Start()
    {
        kunaiRB.velocity = new Vector2(-bulletSpeed, kunaiRB.velocity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {/*
        transform.Translate(0, speed * Time.deltaTime, 0);
        Destroy(this.gameObject, 1.5f);
    }*/
    }
}
