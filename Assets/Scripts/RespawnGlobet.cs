using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGlobet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Set animator ON!!!!!!!!!!!!!!!
            GetComponent<Animator>().SetBool("Flame", true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}