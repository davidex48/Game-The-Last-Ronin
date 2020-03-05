using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Transform posicionInicialKunai;
    public GameObject posicionInicialKunai;
    public GameObject Projectile;
    //public float fireRate = 0.5f;
    //private float elapsedTime;
    // Start is called before the first frame update 


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //elapsedTime += Time.deltaTime; 
        if (Input.GetButtonDown("Fire1"))//&& elapsedTime > fireRate)//GetButton 
        {
            Instantiate(Projectile, posicionInicialKunai.transform.position, posicionInicialKunai.transform.rotation);
        }
        //Instantiate(TexturesKunai, posicionInicialKunai.position, posicionInicialKunai.rotation); 
    }
}