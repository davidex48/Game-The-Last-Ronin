using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< HEAD
    //public Transform posicionInicialKunai;
    public GameObject posicionInicialKunai;
    public GameObject Projectile;
    //public float fireRate = 0.5f;
    private float elapsedTime;
    // Start is called before the first frame update


    void Start()
    {

=======
    public Transform posicionInicialKunai;
    public GameObject TexturesKunai;
    // Start is called before the first frame update
   

    void Start()
    {
        
>>>>>>> master
    }

    // Update is called once per frame
    void FixedUpdate()
<<<<<<< HEAD
    {
       //elapsedTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") )//&& elapsedTime > fireRate)//GetButton
        {
            Instantiate(Projectile, posicionInicialKunai.transform.position, posicionInicialKunai.transform.rotation);
        }
        //Instantiate(TexturesKunai, posicionInicialKunai.position, posicionInicialKunai.rotation);
    }
=======
    {
    if (Input.GetButtonDown("Fire1"))
    {
         Instantiate(TexturesKunai, posicionInicialKunai.position, posicionInicialKunai.rotation);
    }
    }
>>>>>>> master
}

 /*   void PlayerShooting()
    {
   

}*/