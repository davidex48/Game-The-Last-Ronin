using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{ 
    public int index;
    public string levelName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
 
            //Cargar level amb build index
            SceneManager.LoadScene(index);
            //Cargar level pel nom
            SceneManager.LoadScene(levelName);

            //Restart level
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }
}