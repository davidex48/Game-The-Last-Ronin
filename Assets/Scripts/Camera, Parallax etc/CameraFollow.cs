using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] GameObject player;
    private Transform player;
    public float cameraSpeed = 50.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        float posX = Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * cameraSpeed);
       // float posY = Mathf.Lerp(transform.position.y, player.transform.position.y + 0.15f, Time.deltaTime * cameraSpeed);

        float posY = Mathf.Lerp(transform.position.y, transform.position.y + ((player.transform.position.y - transform.position.y) / 100), Time.deltaTime * cameraSpeed * 20);
        //dividiendo entre 100 consigo un movimiento de una centesim aparte de lo que se deberia mover, es decir, la hago progresivo.

        if (transform.position.y - player.transform.position.y < 1 && transform.position.y - player.transform.position.y > 1)   //si movimiento no es o mas grande o mas pequeño que 1 no me mueve la camara
            posY = transform.position.y;
            
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {

    }
}