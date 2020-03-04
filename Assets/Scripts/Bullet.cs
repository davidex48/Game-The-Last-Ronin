using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform posicionInicialKunai;
    public GameObject TexturesKunai;
    // Start is called before the first frame update
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    if (Input.GetButtonDown("Fire1"))
    {
         Instantiate(TexturesKunai, posicionInicialKunai.position, posicionInicialKunai.rotation);
    }
    }
}

 /*   void PlayerShooting()
    {
   

}*/