using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCanvas : MonoBehaviour
{
    //The canvas you want to enable
    public GameObject uiCanvas;
    void Start()
    {
        uiCanvas.SetActive(false);
    }

    //Check if something has entered the collider this script is on
    void OnTriggerEnter2D(Collider2D player)
    {


        if (player.gameObject.tag == "Player")


        {

            uiCanvas.SetActive(true);
            StartCoroutine("WaitForSec");

        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(4);
        Destroy(uiCanvas);
        Destroy(gameObject);
    }
}